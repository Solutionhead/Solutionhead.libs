using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;

namespace Solutionhead.EntityParser
{
    public class EntityParser : IEntityParser
    {
        internal class EntityMetadata
        {
            internal Type Type { get; set; }

            internal EntityType EntityType { get; set; }

            internal List<NavigationProperty> NavigationProperties { get; set; }
        }

        private readonly List<Entity> _entities = new List<Entity>();

        public List<Entity> Entities { get { return _entities.ToList(); } }

        public EntityParser(ObjectContext objectContext)
        {
            if(objectContext == null) { throw new ArgumentNullException("context"); }
            ParseContext(objectContext);
        }

        public EntityParser(IObjectContextAdapter objectContextAdapter)
        {
            if(objectContextAdapter == null) { throw new ArgumentNullException("objectContextAdapter"); }
            ParseContext(objectContextAdapter.ObjectContext);
        }

        private void ParseContext(ObjectContext objectContext)
        {
            var entityMetadata = objectContext.MetadataWorkspace.GetItems(DataSpace.OSpace)
                .Where(i => i.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                .Select(entityType => new EntityMetadata
                {
                    Type = (Type)(entityType.GetType().GetProperty("ClrType", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(entityType, null)),
                    EntityType = (EntityType)entityType,
                    NavigationProperties = ((EntityType)(objectContext.MetadataWorkspace.GetEdmSpaceType((EntityType)entityType))).NavigationProperties.ToList()
                }).ToList();
            BuildEntities(entityMetadata);
        }

        private void BuildEntities(List<EntityMetadata> entityMetadata)
        {
            CreateEntitiesAndProperties(entityMetadata);
            SetEntityNavigationalProperties(entityMetadata);
        }

        private void CreateEntitiesAndProperties(IEnumerable<EntityMetadata> entityMetadata)
        {
            foreach(var entityType in entityMetadata)
            {
                var newEntity = new Entity(entityType.Type);
                CreateEntityProperties(newEntity, entityType.EntityType);
                _entities.Add(newEntity);
            }
        }

        private void SetEntityNavigationalProperties(IEnumerable<EntityMetadata> entityMetadata)
        {
            foreach(var entityType in entityMetadata)
            {
                var newEntity = _entities.First(e => e.Type == entityType.Type);
                foreach(var entityNavProp in entityType.NavigationProperties)
                {
                    var targetEdmTypeAsCollection = entityNavProp.TypeUsage.EdmType as CollectionType;
                    var targetEdmType = targetEdmTypeAsCollection != null ? targetEdmTypeAsCollection.TypeUsage.EdmType : entityNavProp.TypeUsage.EdmType;

                    var newNavProp = new EntityNavigationalProperty(newEntity, entityNavProp.Name, Entities.First(e => e.TypeName == targetEdmType.Name));
                    AddNavigationLinksToEntityProperties(newEntity, newNavProp, entityNavProp);
                    newEntity.NavigationalProperties.Add(newNavProp);
                }
            }
        }

        private void AddNavigationLinksToEntityProperties(Entity entity, EntityNavigationalProperty entityNavigationalProperty, NavigationProperty navigationProperty)
        {
            ReadOnlyMetadataCollection<EdmProperty> destinationProperties;
            ReadOnlyMetadataCollection<EdmProperty> sourceProperties;
            var destination = GetDestinationEntityAndProperties(entity, navigationProperty.RelationshipType as AssociationType, out destinationProperties, out sourceProperties);

            if(destination == null) { return; }

            for(int i = 0; i < sourceProperties.Count; i++)
            {
                var sourceProp = entity.Properties.Find(p => p.Name == sourceProperties[i].Name);
                var destinationProp = destination.Properties.Find(p => p.Name == destinationProperties[i].Name);

                var newNavLink = new EntityPropertyNavigationLink(sourceProp, entityNavigationalProperty, destinationProp);

                entityNavigationalProperty.NavigationalLinks.Add(newNavLink);
                sourceProp.NavigationalLinks.Add(newNavLink);
            }
        }

        private Entity GetDestinationEntityAndProperties(Entity entity, AssociationType association, out ReadOnlyMetadataCollection<EdmProperty> destinationProperties, out ReadOnlyMetadataCollection<EdmProperty> sourceProperties)
        {
            if(!association.ReferentialConstraints.Any())
            {
                destinationProperties = null;
                sourceProperties = null;
                return null;
            }

            var fromRoleType = (association.ReferentialConstraints[0].FromRole.TypeUsage.EdmType as RefType).ElementType;
            var toRoleType = (association.ReferentialConstraints[0].ToRole.TypeUsage.EdmType as RefType).ElementType;

            if(entity.TypeName == fromRoleType.Name || (entity.Type.BaseType != null && entity.Type.BaseType.Name == fromRoleType.Name))
            {
                sourceProperties = association.ReferentialConstraints[0].FromProperties;
                destinationProperties = association.ReferentialConstraints[0].ToProperties;
                return _entities.Find(e => e.TypeName == toRoleType.Name);
            }

            sourceProperties = association.ReferentialConstraints[0].ToProperties;
            destinationProperties = association.ReferentialConstraints[0].FromProperties;
            return _entities.Find(e => e.TypeName == fromRoleType.Name);
        }

        private static void CreateEntityProperties(Entity newEntity, EntityType entityType)
        {
            foreach(var property in entityType.Properties)
            {
                int? keyOrder = null;
                for(var i = 0; i < entityType.KeyMembers.Count; i++)
                {
                    if(entityType.KeyMembers[i].Name != property.Name) continue;
                    keyOrder = i;
                    break;
                }
                newEntity.Properties.Add(new EntityProperty(newEntity, property.Name, keyOrder));
            }
        }
    }
}