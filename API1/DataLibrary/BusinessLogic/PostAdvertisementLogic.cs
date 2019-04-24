using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
	public class PostAdvertisementLogic
	{
		private bool IsVerified { get; set; }

		public async Task<AuthResponseModel> PostAdAsync(AdvertisementModel adModel)
		{
			var result = AdvertisementProcessor.AddPostAsync(adModel);
			var verifyResponse = SetResponse(await result);

			if (IsVerified)
			{
				return SetResponse(5);
			}
			else
			{
				return verifyResponse;
			}
		}

		private AuthResponseModel SetResponse(int decision)
		{
			var jsonResponse = new AuthResponseModel();

			switch (decision)
			{
				case 0:
					jsonResponse.Response = 403;
					jsonResponse.Status = "Error";
					jsonResponse.Info = "Could not complete this action. Contact Administrator.";
					break;
				case 1:
					IsVerified = true;
					break;
				case 3:
					jsonResponse.Response = 500;
					jsonResponse.Status = "Error";
					jsonResponse.Info = "Internal server error";
					break;
				case 5:
					jsonResponse.Response = 200;
					jsonResponse.Status = "Success";
					jsonResponse.Info = "Post added successfully!";
					break;
			}

			return jsonResponse;
		}
	}
}
