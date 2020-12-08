using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Domain.Entities
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage ="mabyc")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "mabyc")]
        public string Description { get; set; }

        [Required(ErrorMessage = "mabyc")]
        [Range(0.01,double.MaxValue)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "mabyc")]
        public string Category { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue =false)]
        public string ImageMimeType { get; set; }

    }
}