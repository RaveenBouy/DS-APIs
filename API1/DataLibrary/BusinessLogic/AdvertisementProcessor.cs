using DataLibrary.DataAccess;
using DataLibrary.Models;
using DataLibrary1.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
	public static class AdvertisementProcessor
	{
		public static async Task<int> AddPostAsync(AdvertisementModel itemModel)
		{
			var sql = $@"INSERT INTO advertisement(ItemName, ItemDescription, ItemCategory,	ItemCount, City, Tele, Price, Con, Negotiable, Created,	Updated) " +
						"VALUES(@ItemName, @ItemDescription, @ItemCategory, @ItemCount, @City, @Tele, @Price, @Con, @Negotiable, @Created, @Updated)";

			var model = new AdvertisementModel
			{
				ItemName = itemModel.ItemName,
				ItemDescription = itemModel.ItemDescription,
				ItemCategory = itemModel.ItemCategory,
				ItemCount = itemModel.ItemCount,
				City = itemModel.City,
				Tele = itemModel.Tele,
				Price = itemModel.Price,
				Con = itemModel.Con,
				Negotiable = itemModel.Negotiable,
			};

            await Task.Run(async () =>
            {
                model.Image = await ImageProcessor.GetImage(itemModel.Image);
            });

            return SqlDataAccess.SaveData(sql, model);
		}

		public static IEnumerable<AdvertisementModel> ViewAllPosts(string searchCondition, string sortCondition, string sortDirection, string location)
		{
			StringBuilder sql = new StringBuilder();

			sql.Append("SELECT * FROM advertisement");

			if (searchCondition.Equals("&none&") && location.Equals("none")) {
				sql.Append(" "+
					$"ORDER BY {sortCondition} {sortDirection}");
			}
			else if(searchCondition.Equals("&none&") && !location.Equals("none")) {
				sql.Append(" WHERE " +
					$"City='{location}' ORDER BY {sortCondition} {sortDirection}");
			}
			else if (!searchCondition.Equals("&none&") && location.Equals("none"))
			{
				sql.Append(" WHERE " +
					$"ItemName LIKE '%{searchCondition}%' ORDER BY {sortCondition} {sortDirection}");
			}
			else if (!searchCondition.Equals("&none&") && !location.Equals("none"))
			{
				sql.Append(" WHERE " +
					$"ItemName LIKE '%{searchCondition}%' AND city= '{location}' ORDER BY {sortCondition} {sortDirection}");
			}

			return SqlDataAccess.LoadData<AdvertisementModel>(sql.ToString());
		}
		

		public static List<AdvertisementModel> ViewPostById(int id)
		{
			var sql = $"SELECT * FROM advertisement WHERE ItemID = {id}";
			
			return SqlDataAccess.LoadData<AdvertisementModel>(sql.ToString());
		}
	}
}
