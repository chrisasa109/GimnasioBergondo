﻿using System.ComponentModel.DataAnnotations;

namespace Gimnasio.Transporte
{
    public class ProductoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Nombre")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Debe tener un mínimo de 6 caracter y un máximo de 50.")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Precio")]
        [Range(0.00, double.MaxValue, ErrorMessage = "El valor de precio no admite valores negativos.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El precio debe tener máximo dos decimales.")]
        [DataType(DataType.Currency)]
        public double Precio { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Stock")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser menor que cero.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La imagen es obligatoria.")]
        [Display(Name ="Foto de producto")]
        public byte[] Foto { get; set; }
    }
}
