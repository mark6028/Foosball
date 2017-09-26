using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foosball
{
    [ModelBinder(typeof(WebApiDataSourceLoadOptionsBinder))]
    public class WebApiDataSourceLoadOptions : DataSourceLoadOptionsBase
    {
    }


    public class WebApiDataSourceLoadOptionsBinder : IModelBinder
    {

        public bool BindModel(ModelBindingContext bindingContext)
        {
            var loadOptions = new WebApiDataSourceLoadOptions();

            DataSourceLoadOptionsParser.Parse(loadOptions, key => {
                var value = bindingContext.ValueProvider.GetValue(key);
                if (value != null)
                    return value.FirstValue;

                return null;
            });

            bindingContext.Model = loadOptions;
            return true;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var loadOptions = new WebApiDataSourceLoadOptions();

            DataSourceLoadOptionsParser.Parse(loadOptions, key => {
                var value = bindingContext.ValueProvider.GetValue(key);
                if (value != null)
                    return value.FirstValue;

                return null;
            });

            //bindingContext.Model = loadOptions;
            bindingContext.Result = ModelBindingResult.Success(loadOptions);

            await Task.FromResult(true);
        }
    }
}
