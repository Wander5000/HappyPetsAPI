using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HappyPetsAPI.Context;
using HappyPetsAPI.DTOs.Categoria;

namespace HappyPetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly HappyPetsDbContext _context;

        public CategoriasController(HappyPetsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> ListarCategorias()
        {
            return await _context.Categorias
                .Select(c => new CategoriaDTO
                {
                    IdCategoria = c.IdCategoria,
                    NombreCategoria = c.NombreCategoria,
                    Descripcion = c.Descripcion
                })
                .ToListAsync();
        }
    }
}
