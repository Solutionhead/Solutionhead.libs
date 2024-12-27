using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EntityParser;

// Here's a discussion thread of someone dealing with the same issue that this utility
// addresses http://autofixture.codeplex.com/discussions/262557
namespace Solutionhead.EntityParser
{
    public class EntityObjectGraphForeignKeyConstrainer : IEntityObjectGraphForeignKeyConstrainer
    {
        private List<Entity> Entities { get; set; }

        private Dictionary<EntityProperty, List<object>> ObjectsWithKeyPropertySet { get; set; }

        public EntityObjectGraphForeignKeyConstrainer(IEnumerable<Entity> entities)
        {
            Entities = entities.ToList();
        }

        public TObject ConstrainForeignKeys<TObject>(TObject @object)
        {
            ObjectsWithKeyPropertySet = new Dictionary<EntityProperty, List<object>>();
            Entities.SelectMany(e => e.Properties.Where(p => p.IsKey)).ToList().ForEach(p => ObjectsWithKeyPropertySet.Add(p, new List<object>()));
            SetForeignKeysThroughoutObjectGraph(@object);
            return @object;
        }

        private void SetForeignKeysThroughoutObjectGraph(object @object)
        {
            var objectType = @object.GetType();
            var entity = Entities.FirstOrDefault(e => e.Type == objectType);
            if(entity == null) { return; }

            foreach(var property in entity.Properties.Where(p => p.IsKey))
            {
                if(!ObjectsWithKeyPropertySet[property].Contains(@object))
                {
                    ObjectsWithKeyPropertySet[property].Add(@object);
                }
            }
            
            foreach(var navProp in entity.NavigationalProperties)
            {
                SetForeignKeys(@object, navProp);
            }
        }

        private void SetForeignKeys(object @object, EntityNavigationalProperty navProp)
        {
            var navPropObject = navProp.GetValue(@object);
            if(navPropObject == null) { return; }

            foreach(var navigationalLink in navProp.NavigationalLinks)
            {
                if(navProp.IsCollection)
                {
                    var sourceValue = navigationalLink.Source.GetValue(@object);
                    foreach(var element in (ICollection)navPropObject)
                    {
                        if(element == null)
                        {
                            continue;
                        }

                        navigationalLink.Destination.SetValue(element, sourceValue);
                        if(navigationalLink.Destination.IsKey)
                        {
                            ObjectsWithKeyPropertySet[navigationalLink.Destination].Add(element);
                        }
                    }
                }
                else
                {
                    var sourceValue = navigationalLink.Source.GetValue(@object);
                    navigationalLink.Destination.SetValue(navPropObject, sourceValue);
                    if(navigationalLink.Destination.IsKey)
                    {
                        ObjectsWithKeyPropertySet[navigationalLink.Destination].Add(navPropObject);
                    }
                }
            }

            if(navProp.IsCollection)
            {
                var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(navPropObject.GetType().GetGenericArguments()[0])) as IList;
                foreach(var element in (ICollection)navPropObject)
                {
                    if(element != null)
                    {
                        if(PrimaryKeysAreTaken(element))
                        {
                            ObjectsWithKeyPropertySet.ToList().ForEach(k => k.Value.RemoveAll(o => o == element));
                        }
                        else
                        {
                            SetForeignKeysThroughoutObjectGraph(element);
                            list.Add(element);
                        }
                    }
                    navProp.SetValue(@object, (list != null && list.Count > 0) ? list : null);
                }
            }
            else
            {
                if(PrimaryKeysAreTaken(navPropObject))
                {
                    ObjectsWithKeyPropertySet.ToList().ForEach(k => k.Value.RemoveAll(o => o == navPropObject));
                    navProp.SetValue(@object, null);
                }
                else
                {
                    SetForeignKeysThroughoutObjectGraph(navPropObject);
                }
            }
        }

        private bool PrimaryKeysAreTaken(object @object)
        {
            var objectType = @object.GetType();
            var keyPropertiesWithObjects = ObjectsWithKeyPropertySet.Where(po => po.Key.Parent.Type == objectType).ToList();
            var otherObjectsWithAKeyPropertySet = keyPropertiesWithObjects.SelectMany(k => k.Value).Where(o => o != @object).Distinct().ToList();
            var objectsWithAllKeysSet = otherObjectsWithAKeyPropertySet.Where(o => keyPropertiesWithObjects.All(k => k.Value.Contains(o))).ToList();
            return objectsWithAllKeysSet.Any(o => keyPropertiesWithObjects.All(k => k.Key.GetValue(o).Equals(k.Key.GetValue(@object))));
        }

        //private bool ObjectHasAllNavigationalLinkPropertiesSet(EntityNavigationalProperty navigationalProperty, object @object)
        //{
        //    return navigationalProperty.NavigationalLinks.All(l => ObjectsWithKeyPropertySet[l.Destination].Contains(@object));
        //}

        //private bool ObjectHasAllPrimaryKeysSet(object @object)
        //{
        //    var entity = Entities.FirstOrDefault(e => e.Type == @object.GetType());
        //    if(entity == null)
        //    {
        //        throw new ArgumentNullException(string.Format("Could not find entity for object of type '{0}'", @object.GetType()));
        //    }
        //    return ObjectsWithKeyPropertySet.Where(k => k.Key.IsKey).All(o => o.Value.Contains(@object));
        //}
    }
}