using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryCell), typeof(CompucareWard.iOS.Renderers.StandardEntryCellRenderer))]
namespace CompucareWard.iOS.Renderers
{
    public class StandardEntryCellRenderer : EntryCellRenderer
    {
        public override UIKit.UITableViewCell GetCell(Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            switch (item.StyleId)
            {
                case "checkmark":
                    cell.Accessory = UIKit.UITableViewCellAccessory.Checkmark;
                    break;
                case "detail-button":
                    cell.Accessory = UIKit.UITableViewCellAccessory.DetailButton;
                    break;
                case "detail-disclosure-button":
                    cell.Accessory = UIKit.UITableViewCellAccessory.DetailDisclosureButton;
                    break;
                case "disclosure":
                    cell.Accessory = UIKit.UITableViewCellAccessory.DisclosureIndicator;
                    break;
                case "none":
                default:
                    cell.Accessory = UIKit.UITableViewCellAccessory.None;
                    break;
            }

            return cell;
        }
    }
}
