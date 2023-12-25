﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IDoctorService : IGenericService<Doctor>
    {
        List<Doctor> GetByDepartmentID(int id);
        List<Doctor> ListDoctorWithDepartment();
    }
}
