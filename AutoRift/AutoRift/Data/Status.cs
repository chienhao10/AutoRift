using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace AutoRift.Data
{
    public class Status : IEnumerable<string>
    {
        public Queue<string> StatusMessages { get; set; }
        public int Size { get; set; }
        private const string Indent = "  ";

        public Status(int size = 5)
        {
            StatusMessages = new Queue<string>();
            Size = size;
        }

        public void Log(string message, int indent = 0)
        {
            while (StatusMessages.Count - 1 > Size)
            {
                RemoveLast();
            }
            var indentValue = "";
            for (var i = 0; i < indent; i++)
            {
                indentValue += Indent;
            }
            StatusMessages.Enqueue(indentValue + message);
        }

        public void RemoveLast()
        {
            if (StatusMessages.Count > 0)
            {
                StatusMessages.Dequeue();
            }
        }
        
        public IEnumerator<string> GetEnumerator()
        {
            return StatusMessages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}