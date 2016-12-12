using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Models.ViewModels
{
    public class RequestInfo
    {
        public RequestInfo(string statusCode)
        {
            SetMessage(statusCode);
        }

        void SetMessage(string statusCode)
        {
            switch (statusCode)
            {
                case "200":
                    this.Message = "Успешно";
                    this.isValid = true;
                    break;
                case "OK":
                    this.Message = "Успешно";
                    this.isValid = true;
                    break;
                case "BadRequest":
                    this.Message = "Ошибка на сервере";
                    this.isValid = false;
                    break;
                case "BadInternet":
                    this.Message = "Нет интернета";
                    this.isValid = false;
                    break;
                case "Empty":
                    this.Message = "Введите данные!";
                    this.isValid = false;
                    break;
                case "invalidEmail":
                    this.Message = "Email написан не верно";
                    this.isValid = false;
                    break;
                case "500":
                    this.Message = "Ошибка на сервере";
                    this.isValid = false;
                    break;
                case "404":
                    this.Message = "Не найдено";
                    this.isValid = false;
                    break;
                default:
                    this.Message = "добавить еще коды ошибок)";
                    this.isValid = false;
                    break;
            }
        }

        public bool isValid { get; set; }
        public string Message { get; set; }
    }
}
