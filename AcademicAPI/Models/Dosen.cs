
using System.ComponentModel.DataAnnotations;

namespace AcademicApi.Models
{
    public class Dosen
    {
        public int DosenId { get; set; }

        [Required(ErrorMessage = "NIDN wajib diisi.")]
        [StringLength(20)]
        public string Nidn { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nama dosen wajib diisi.")]
        [StringLength(100)]
        public string NamaDosen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email wajib diisi.")]
        [EmailAddress(ErrorMessage = "Format email tidak valid.")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Program studi wajib diisi.")]
        [StringLength(100)]
        public string Prodi { get; set; } = string.Empty;

        public List<Mahasiswa> MahasiswaWali { get; set; } = new();
    }
}
