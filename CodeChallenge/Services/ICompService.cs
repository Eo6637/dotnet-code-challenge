﻿using CodeChallenge.Models;
using System;

namespace CodeChallenge.Services
{
    public interface ICompService
    {
        Compensation GetById(String id);
        Compensation Create(Compensation compensation);
    }
}
