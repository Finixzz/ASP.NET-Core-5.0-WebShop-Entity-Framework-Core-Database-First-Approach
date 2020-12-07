using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class ItemPicture
    {
        public int ItemPictureId { get; set; }
        public int ItemId { get; set; }
        public string FilePath { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual Item Item { get; set; }
    }
}
