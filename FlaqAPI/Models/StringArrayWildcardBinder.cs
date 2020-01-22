using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace FlaqAPI.Models
{
    public class StringArrayWildcardBinder : IModelBinder
    {
         public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
            {
                var key = bindingContext.ModelName;
                var val = bindingContext.ValueProvider.GetValue(key);
                if (val != null)
                {
                    var s = val.AttemptedValue;
                    if (s != null)
                    {
                        try
                        {
                        // STEP 1: do your processing on the attempted value, and save the final
                        //         object to the bindingContext.Model
                        // TODO: for this sample we just copy the string, your logic 
                        //         should parse/process it
                            var array = s.Split('/');
                            bindingContext.Model = array;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
        
    }
}