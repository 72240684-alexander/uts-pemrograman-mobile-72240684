
using AcademicApi.Data;
using AcademicApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DosenController : ControllerBase
    {
        private readonly AcademicDbContext _context;

        public DosenController(AcademicDbContext context)
        {
            _context = context;
        }

        // GET: api/dosen
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dosen = await _context.Dosen
                .Select(d => new
                {
                    d.DosenId,
                    d.Nidn,
                    d.NamaDosen,
                    d.Email,
                    d.Prodi
                })
                .ToListAsync();

            return Ok(dosen);
        }

        // GET: api/dosen/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dosen = await _context.Dosen
                .Where(d => d.DosenId == id)
                .Select(d => new
                {
                    d.DosenId,
                    d.Nidn,
                    d.NamaDosen,
                    d.Email,
                    d.Prodi
                })
                .FirstOrDefaultAsync();

            if (dosen == null)
            {
                return NotFound(new
                {
                    pesan = "Data dosen tidak ditemukan."
                });
            }

            return Ok(dosen);
        }

        // POST: api/dosen
        [HttpPost]
        public async Task<IActionResult> Create(Dosen dosen)
        {
            bool nidnAda = await _context.Dosen
                .AnyAsync(d => d.Nidn == dosen.Nidn);

            if (nidnAda)
            {
                return Conflict(new
                {
                    pesan = "NIDN sudah digunakan."
                });
            }

            _context.Dosen.Add(dosen);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = dosen.DosenId },
                new
                {
                    dosen.DosenId,
                    dosen.Nidn,
                    dosen.NamaDosen,
                    dosen.Email,
                    dosen.Prodi
                });
        }

        // PUT: api/dosen/1

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id, Dosen dataDosen)
        {
            var dosen = await _context.Dosen.FindAsync(id);

            if (dosen == null)
            {
                return NotFound(new
                {
                    pesan = "Data dosen tidak ditemukan."
                });
            }

            bool nidnDipakai = await _context.Dosen
                .AnyAsync(d =>
                    d.Nidn == dataDosen.Nidn &&
                    d.DosenId != id);

            if (nidnDipakai)
            {
                return Conflict(new
                {
                    pesan = "NIDN sudah digunakan oleh dosen lain."
                });
            }

            dosen.Nidn = dataDosen.Nidn;
            dosen.NamaDosen = dataDosen.NamaDosen;
            dosen.Email = dataDosen.Email;
            dosen.Prodi = dataDosen.Prodi;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                pesan = "Data dosen berhasil diperbarui."
            });
        }


        // DELETE: api/dosen/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dosen = await _context.Dosen
                .FindAsync(id);

            if (dosen == null)
            {
                return NotFound(new
                {
                    pesan = "Data dosen tidak ditemukan."
                });
            }

            bool memilikiMahasiswa = await _context.Mahasiswa
                .AnyAsync(m => m.DosenId == id);

            if (memilikiMahasiswa)
            {
                return Conflict(new
                {
                    pesan = "Dosen tidak dapat dihapus karena masih menjadi dosen wali mahasiswa."
                });
            }

            _context.Dosen.Remove(dosen);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                pesan = "Data dosen berhasil dihapus."
            });
        }
    }
}