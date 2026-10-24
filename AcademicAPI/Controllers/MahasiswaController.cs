
using AcademicApi.Data;
using AcademicApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AcademicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MahasiswaController : ControllerBase
    {
        private readonly AcademicDbContext _context;

        public MahasiswaController(AcademicDbContext context)
        {
            _context = context;
        }

        // GET: api/mahasiswa
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mahasiswa = await _context.Mahasiswa
                .Select(m => new
                {
                    m.MahasiswaId,
                    m.Nim,
                    m.NamaMahasiswa,
                    m.Email,
                    m.Angkatan,
                    m.DosenId,
                    NamaDosenWali = m.DosenWali!.NamaDosen
                })
                .ToListAsync();

            return Ok(mahasiswa);
        }

        // GET: api/mahasiswa/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var mahasiswa = await _context.Mahasiswa
                .Where(m => m.MahasiswaId == id)
                .Select(m => new
                {
                    m.MahasiswaId,
                    m.Nim,
                    m.NamaMahasiswa,
                    m.Email,
                    m.Angkatan,
                    m.DosenId,
                    NamaDosenWali = m.DosenWali!.NamaDosen
                })
                .FirstOrDefaultAsync();

            if (mahasiswa == null)
            {
                return NotFound(new
                {
                    pesan = "Data mahasiswa tidak ditemukan."
                });
            }

            return Ok(mahasiswa);
        }

        // POST: api/mahasiswa
        [HttpPost]
        public async Task<IActionResult> Create(Mahasiswa mahasiswa)
        {
            bool nimAda = await _context.Mahasiswa
                .AnyAsync(m => m.Nim == mahasiswa.Nim);

            if (nimAda)
            {
                return Conflict(new
                {
                    pesan = "NIM sudah digunakan."
                });
            }

            bool dosenAda = await _context.Dosen
                .AnyAsync(d => d.DosenId == mahasiswa.DosenId);

            if (!dosenAda)
            {
                return BadRequest(new
                {
                    pesan = "Dosen wali yang dipilih tidak tersedia."
                });
            }

            // DosenWali ditentukan melalui DosenId.
            mahasiswa.DosenWali = null;

            _context.Mahasiswa.Add(mahasiswa);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is SqliteException sqliteEx
                    && sqliteEx.SqliteErrorCode == 19)
            {
                return Conflict(new
                {
                    pesan = "NIM sudah digunakan atau data mahasiswa melanggar aturan database."
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = mahasiswa.MahasiswaId },
                new
                {
                    mahasiswa.MahasiswaId,
                    mahasiswa.Nim,
                    mahasiswa.NamaMahasiswa,
                    mahasiswa.Email,
                    mahasiswa.Angkatan,
                    mahasiswa.DosenId
                });
        }

        // PUT: api/mahasiswa/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id, Mahasiswa dataMahasiswa)
        {
            var mahasiswa = await _context.Mahasiswa
                .FindAsync(id);

            if (mahasiswa == null)
            {
                return NotFound(new
                {
                    pesan = "Data mahasiswa tidak ditemukan."
                });
            }

            bool nimDipakai = await _context.Mahasiswa
                .AnyAsync(m =>
                    m.Nim == dataMahasiswa.Nim &&
                    m.MahasiswaId != id);

            if (nimDipakai)
            {
                return Conflict(new
                {
                    pesan = "NIM sudah digunakan oleh mahasiswa lain."
                });
            }

            bool dosenAda = await _context.Dosen
                .AnyAsync(d => d.DosenId == dataMahasiswa.DosenId);

            if (!dosenAda)
            {
                return BadRequest(new
                {
                    pesan = "Dosen wali yang dipilih tidak tersedia."
                });
            }

            mahasiswa.Nim = dataMahasiswa.Nim;
            mahasiswa.NamaMahasiswa = dataMahasiswa.NamaMahasiswa;
            mahasiswa.Email = dataMahasiswa.Email;
            mahasiswa.Angkatan = dataMahasiswa.Angkatan;
            mahasiswa.DosenId = dataMahasiswa.DosenId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is SqliteException sqliteEx
                    && sqliteEx.SqliteErrorCode == 19)
            {
                return Conflict(new
                {
                    pesan = "NIM sudah digunakan atau data mahasiswa melanggar aturan database."
                });
            }

            return Ok(new
            {
                pesan = "Data mahasiswa berhasil diperbarui."
            });
        }

        // DELETE: api/mahasiswa/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var mahasiswa = await _context.Mahasiswa
                .FindAsync(id);

            if (mahasiswa == null)
            {
                return NotFound(new
                {
                    pesan = "Data mahasiswa tidak ditemukan."
                });
            }

            _context.Mahasiswa.Remove(mahasiswa);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                pesan = "Data mahasiswa berhasil dihapus."
            });
        }
    }
}
