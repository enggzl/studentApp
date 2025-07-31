using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Ogrenci
    {
        [Key]
        public int OgrenciId { get; set; }
        [Required(ErrorMessage = "Öğrenci adı boş geçilemez.")]
        public string? OgrenciAd { get; set; }
        [Required(ErrorMessage = "Öğrenci soyadı boş geçilemez.")]
        [StringLength(50, ErrorMessage = "Soyad 50 karakterden fazla olamaz.")]

        [Display(Name = "Soyad")]
        public string? OgrenciSoyad { get; set; }

        public string AdSoyad
        {
            get
            {
                return this.OgrenciAd + " " + this.OgrenciSoyad;
            }
        }

        [Required(ErrorMessage = "E-posta boş geçilemez.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [Display(Name = "E-posta")]
        public string? Eposta { get; set; }
        [Required(ErrorMessage = "Telefon numarası boş geçilemez.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string? Telefon { get; set; }

        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();
    }
}