using kolos.DTOs;
using kolos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolos.Services
{
    public interface IDbService
    {
        PrescriptionDto GetPrescription(int id);
        Prescription AddPrescription(PrescriptionRequest p);
    }
}
