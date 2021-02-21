using kolos.DTOs;
using kolos.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolos.Services
{
    public class MyDbService : IDbService
    {
        private readonly s17060Context _context;
        public MyDbService(s17060Context context)
        {
            _context = context;
        }
        public Prescription AddPrescription(PrescriptionRequest request)
        {
            var prescr = new Prescription()
            {
                Date = request.Date,
                DueDate = request.DueDate,
                IdDoctor = request.IdDoctor,
                IdPatient = request.IdPatient,
                IdPrescription = 20
            };

            //_context.Prescriptions.InsertOnSubmit(prescr);

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17060;Integrated Security=True;MultipleActiveResultSets=true"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var transaction = con.BeginTransaction();
                com.Transaction = transaction;

                try
                {
                    com.CommandText = "INSERT INTO Prescription(IdPrescription, Date, DueDate, IdPatient, IdDoctor) VALUES(@idprescr, @date, @duedate, @idpatient, @iddoctor)";
                    com.Parameters.AddWithValue("idprescr", 20);
                    com.Parameters.AddWithValue("date", prescr.Date);
                    com.Parameters.AddWithValue("duedate", prescr.DueDate);
                    com.Parameters.AddWithValue("idpatient", prescr.IdPatient);
                    com.Parameters.AddWithValue("iddoctor", prescr.IdDoctor);


                    com.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException e)
                {
                    transaction.Rollback();
                }
                finally
                {
                    con.Close();
                }
            }



            return prescr;
        }

        public PrescriptionDto GetPrescription(int id)
        {


            var prescr = _context.Prescriptions.SingleOrDefault(p => p.IdPrescription == id);
            var prescrmeds = _context.PrescriptionMedicaments.Where(p => p.IdPrescription == id).Select(p => p.IdMedicament).ToList();
            var meds = _context.Medicaments.Where(p => prescrmeds.Contains(p.IdMedicament));

            PrescriptionDto prescrDto = new PrescriptionDto
            {
                IdDoctor = prescr.IdDoctor,
                IdPatient = prescr.IdPatient,
                IdPrescription = prescr.IdPrescription,
                Date = prescr.Date,
                DueDate = prescr.DueDate,
                Medicaments = new List<MedicamentDto>()
            };

            foreach(Medicament m in meds)
            {
                prescrDto.Medicaments.Add(new MedicamentDto
                {
                    IdMedicament = m.IdMedicament,
                    Description = m.Description,
                    Name = m.Name,
                    Type = m.Type
                });
            }

            return prescrDto;
        }
    }
}
