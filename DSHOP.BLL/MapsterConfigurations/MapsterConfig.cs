using DSHOP.DAL.DTO.Response;
using DSHOP.DAL.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.BLL.MapsterConfigurations
{
    public static class MapsterConfig
    {
        public static void MapsterConfRegister()
        {
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
                .Map(dest => dest.CreatedBy, source => source.User.UserName);

            TypeAdapterConfig<Category, CategoryUserResponse>.NewConfig()
                .Map(dest => dest.Name, source => source.Translations
                .Where(t => t.Language == MapContext.Current.Parameters["lang"].ToString())
                .Select(t=>t.Name).FirstOrDefault());

            TypeAdapterConfig<Product,ProductResponse>.NewConfig()
                .Map(dest=>dest.MainImage, source => $"http://localhost:5051/Images/{source.MainImage}");

            TypeAdapterConfig<Product, ProductUserResponse>.NewConfig()
                .Map(dest => dest.MainImage, source => $"http://localhost:5051/Images/{source.MainImage}")
                .Map(dest => dest.Name, source => source.Translations
                .Where(t => t.Language == MapContext.Current.Parameters["lang"].ToString())
                .Select(t => t.Name).FirstOrDefault());

            TypeAdapterConfig<Product, ProductUserDetails>.NewConfig()
                .Map(dest => dest.MainImage, source => $"http://localhost:5051/Images/{source.MainImage}")
                .Map(dest => dest.Name, source => source.Translations
                .Where(t => t.Language == MapContext.Current.Parameters["lang"].ToString())
                .Select(t => t.Name).FirstOrDefault())
                .Map(dest => dest.Description, source => source.Translations
                .Where(t => t.Language == MapContext.Current.Parameters["lang"].ToString())
                .Select(t => t.Name).FirstOrDefault());
        }
    }
}
