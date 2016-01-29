using ScrumBoard.Domain.Entities;
using ScrumBoard.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ScrumBoard.DAL
{
    public class BoardInMemoryRepository : IRepository<Board>
    {
        static private List<Board> _boards;
        public List<Board> Boards
        {
            get
            {
                if (_boards == null)
                    _boards = new List<Board>();
                return _boards;
            }
        }

        public Board Find(int id)
        {
            return this.Boards.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Board> List()
        {
            return this.Boards;
        }

        public void Save(Board board)
        {
            board.Id = this.Boards.Count() + 1;
            Boards.Add(board);
        }
    }
}
