using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Unit18.Commands
{
    public interface ICommand
    {
        Task<bool> ExecuteAsync();

        void Cancel();
    }
}
