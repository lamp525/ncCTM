
namespace CTM.Win.Models
{
    public class ComboBoxItemModel
    {
        public string Text { set; get; }

        public string Value { set; get; }

        public override string ToString()
        {
            return Text;
        }
    }
}