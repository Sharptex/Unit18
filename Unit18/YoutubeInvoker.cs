using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unit18.Commands;

namespace Unit18
{
    class YoutubeInvoker
    {
        ICommand _command;

        public void SetCommand(ICommand command)
        {
            _command = command;
        }

        public async Task<bool> ExecuteAsync()
        {
            return await  _command.ExecuteAsync();
        }

        public void Cancel()
        {
            _command.Cancel();
        }
    }
}
