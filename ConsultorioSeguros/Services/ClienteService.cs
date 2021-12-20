using ConsultorioSeguros.Models.API;
using ConsultorioSeguros.Models.SQL;
using ConsultorioSeguros.Models.ViewData;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ConsultorioSeguros.Services
{
    public class ClienteService
    {
        private readonly ConsultorioDbContext _consultorioDbContext = new ConsultorioDbContext("ServerConnection");

        public ReplyDatatable GetClientesData(HttpRequest contextRequest)
        {
            ReplyDatatable reply = new ReplyDatatable
            {
                Data = null,
                Message = string.Empty,
                Result = 0,
                Draw = 0,
                RecordsFiltered = 0,
                RecordsTotal = 0
            };

            try
            {
                int draw = Convert.ToInt32(contextRequest.Form["draw"]);

                int start = Convert.ToInt32(contextRequest.Form["start"]);
                int length = Convert.ToInt32(contextRequest.Form["length"]);

                string sortColumn = contextRequest.Form["columns[" + contextRequest.Form["order[0][column]"] + "][name]"];
                string sortColumnDirection = contextRequest.Form["order[0][dir]"];
                string searchValue = contextRequest.Form["search[value]"];


                List<Cliente> data = new List<Cliente>();
                int recordsTotal = 0;
                int recordsFiltered = 0;

                data = _consultorioDbContext.Cliente.OrderBy(a => a.Cedula).ToList();

                recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(searchValue))//filter
                {
                    data = data.Where(a =>
                        a.Cedula.ToString().ToUpper().Contains(searchValue.ToUpper()) ||
                        a.Nombre.ToUpper().Contains(searchValue.ToUpper()) ||
                        a.Telefono.Contains(searchValue.ToUpper()) ||
                        a.Edad.ToString().Contains(searchValue.ToUpper())
                    ).ToList();
                }

                recordsFiltered = data.Count;

                //sorting
                if (sortColumnDirection.Equals("asc"))
                {
                    if (sortColumn.Equals("Cedula"))
                    {
                        data = data.OrderBy(i => i.Cedula).ToList();
                    }
                    else if (sortColumn.Equals("Nombre"))
                    {
                        data = data.OrderBy(i => i.Nombre).ToList();
                    }
                    else if (sortColumn.Equals("Telefono"))
                    {
                        data = data.OrderBy(i => i.Telefono).ToList();
                    }
                    else if (sortColumn.Equals("Edad"))
                    {
                        data = data.OrderBy(i => i.Edad).ToList();
                    }
                }
                if (sortColumnDirection.Equals("desc"))
                {
                    if (sortColumn.Equals("Cedula"))
                    {
                        data = data.OrderByDescending(i => i.Cedula).ToList();
                    }
                    else if (sortColumn.Equals("Nombre"))
                    {
                        data = data.OrderByDescending(i => i.Nombre).ToList();
                    }
                    else if (sortColumn.Equals("Telefono"))
                    {
                        data = data.OrderByDescending(i => i.Telefono).ToList();
                    }
                    else if (sortColumn.Equals("Edad"))
                    {
                        data = data.OrderByDescending(i => i.Edad).ToList();
                    }
                }

                //paging
                data = data.Skip(start).Take(length).ToList();

                reply.Data = data;
                reply.Result = 1;

                reply.Draw = draw;
                reply.RecordsFiltered = recordsFiltered;
                reply.RecordsTotal = recordsTotal;
            }
            catch (Exception e)
            {
                reply.Message = e.GetBaseException().Message;
            }

            return reply;
        }

        public Reply AddCliente(ClienteViewData input)
        {
            Reply reply = new Reply
            {
                Data = null,
                Message = string.Empty,
                Result = 0
            };

            using (DbContextTransaction transaction = _consultorioDbContext.Database.BeginTransaction())
            {
                try
                {
                    Cliente cliente = new Cliente
                    {
                        Cedula = input.Cedula.ToUpper(),
                        Nombre = input.Nombre,
                        Telefono = input.Telefono,
                        Edad = input.Edad
                    };

                    _consultorioDbContext.Cliente.Add(cliente);
                    _consultorioDbContext.SaveChanges();

                    transaction.Commit();

                    reply.Data = cliente;
                    reply.Message = "Cliente " + cliente.Cedula + " creado con éxito";
                    reply.Result = 1;
                }
                catch (Exception e)
                {
                    transaction.Rollback();

                    reply.Message = e.GetBaseException().Message;
                }
            }
            return reply;
        }

        public Reply UpdateCliente(ClienteViewData input)
        {
            Reply reply = new Reply
            {
                Data = null,
                Message = string.Empty,
                Result = 0
            };

            using (DbContextTransaction transaction = _consultorioDbContext.Database.BeginTransaction())
            {
                try
                {
                    Cliente cliente = _consultorioDbContext.Cliente.Where(a => a.Cedula.Equals(input.Cedula.ToUpper())).SingleOrDefault();
                    if (cliente != null)
                    {
                        cliente.Nombre = input.Nombre;
                        cliente.Telefono = input.Telefono;
                        cliente.Edad = input.Edad;

                        _consultorioDbContext.Entry(cliente).State = EntityState.Modified;
                        _consultorioDbContext.SaveChanges();
                    }

                    transaction.Commit();

                    reply.Data = cliente;
                    reply.Message = "Cliente " + cliente.Cedula + " actualizado con éxito";
                    reply.Result = 1;
                }
                catch (Exception e)
                {
                    transaction.Rollback();

                    reply.Message = e.GetBaseException().Message;
                }
            }
            return reply;
        }

        public Reply DeleteCliente(ClienteViewData input)
        {
            Reply reply = new Reply
            {
                Data = null,
                Message = string.Empty,
                Result = 0
            };

            using (DbContextTransaction transaction = _consultorioDbContext.Database.BeginTransaction())
            {
                try
                {
                    Cliente cliente = _consultorioDbContext.Cliente.Where(a => a.Cedula.Equals(input.Cedula.ToUpper())).SingleOrDefault();
                    if (cliente != null)
                    {
                        _consultorioDbContext.Cliente.Remove(cliente);
                        _consultorioDbContext.Entry(cliente).State = EntityState.Deleted;
                        _consultorioDbContext.SaveChanges();
                    }

                    transaction.Commit();

                    reply.Data = cliente;
                    reply.Message = "El seguro " + cliente.Cedula + " ha sido eliminado";
                    reply.Result = 1;
                }
                catch (Exception e)
                {
                    transaction.Rollback();

                    reply.Message = e.GetBaseException().Message;
                }
            }
            return reply;
        }

        public Reply UploadCliente(HttpRequest Request)
        {
            Reply reply = new Reply
            {
                Data = null,
                Message = string.Empty,
                Result = 0
            };

            HttpPostedFile fileBase = Request.Files[0];

            List<Cliente> clientes = new List<Cliente>();

            using (var package = new ExcelPackage(fileBase.InputStream))
            {
                // obtener la primera hoja de trabajo del libro
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                for (int row = 2; worksheet.Cells[row, 1].Value != null; row++)
                {
                    string A = worksheet.Cells[row, 1].Value != null ? worksheet.Cells[row, 1].Value.ToString().Trim() : "0";
                    string B = worksheet.Cells[row, 2].Value != null ? worksheet.Cells[row, 2].Value.ToString().Trim() : "0";
                    string C = worksheet.Cells[row, 3].Value != null ? worksheet.Cells[row, 3].Value.ToString().Trim() : "0";
                    string D = worksheet.Cells[row, 4].Value != null ? worksheet.Cells[row, 4].Value.ToString().Trim() : "0";

                    clientes.Add(new Cliente
                    {
                        Cedula = A,
                        Nombre = B,
                        Edad = Convert.ToInt32(C),
                        Telefono = D
                    });
                }
            }

            int count = clientes.Count;

            if (count > 0)
            {
                using (DbContextTransaction transaction = _consultorioDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        //foreach(Cliente c in clientes)
                        //{
                        //    Cliente cliente = new Cliente
                        //    {
                        //        Cedula = c.Cedula,
                        //        Nombre = c.Nombre,
                        //        Telefono = c.Telefono,
                        //        Edad = c.Edad
                        //    };

                        //    _consultorioDbContext.Cliente.Add(cliente);
                        //}
                        _consultorioDbContext.Cliente.AddRange(clientes);
                        _consultorioDbContext.SaveChanges();

                        transaction.Commit();


                        if (count == 1)
                        {
                            reply.Message = "Se cargó " + count + " cliente";
                        }
                        else
                        {
                            reply.Message = "Se cargaron " + count + " clientes";
                        }

                        reply.Result = 1;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();

                        reply.Message = e.GetBaseException().Message;
                    }
                }
            }
            else
            {
                reply.Message = "No hay datos de clientes para cargar";
            }

            return reply;
        }
    }
}