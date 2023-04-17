﻿using Academico.Data;
using Academico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;    
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Academico.Controllers
{
    public class InstituicaoController : Controller
    {
        private readonly AcademicoContext _context;

        public InstituicaoController(AcademicoContext context)
        {
            _context = context;
        }
        public bool InstituicaoExists (long? id)
        {
            return _context.Instituicoes.Any(i => i.Id == id);  
        }

        

    public async Task<IActionResult> Index()
    {
        return View(await _context.Instituicoes.OrderBy(i => i.Nome).ToListAsync());
    }

    //GET: Departamento/Create
        public IActionResult Create()
        { 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Nome", "Endereco")]Instituicao instituicao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                _context.Add(instituicao);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
                }
            }
        catch(DbUpdateException) 
        {
            ModelState.AddModelError("", "Não foi possível cadastrar a instituição.");
        }
        return View(instituicao);
        }
        //GET: Instituicao/Edit/id

        public async Task<ActionResult> Edit(long id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(i => i.Id == id);
            if (instituicao == null)
            {
                return NotFound();
            }
            return View(instituicao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Edit(long? id,[Bind("Id", "Nome", "Endereco")] Instituicao instituicao)
        {
            if (id != instituicao.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid) 
            { 
                try
                {
                    _context.Update(instituicao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) 
                { 
                    if (!InstituicaoExists(instituicao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(instituicao);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var instuicao = await _context.Instituicoes.SingleOrDefaultAsync(i => i.Id == id);
            if (instuicao == null)
            {
                return NotFound();
            }
            return View(instuicao);
        }
        //GET: Instituicao/Delete/id

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null) 
            {
                return NotFound();
            }
            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(i => i.Id == id);
            if(instituicao == null)
            {
                return NotFound();
            }
            return View(instituicao);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> DeleteConfirmed(long? id)
        {
            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(i => i.Id == id);
            _context.Instituicoes.Remove(instituicao);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
