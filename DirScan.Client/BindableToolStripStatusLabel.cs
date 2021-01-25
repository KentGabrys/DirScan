using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace DirScan.Client
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.StatusStrip)]
    public class BindableToolStripStatusLabel : ToolStripStatusLabel, IBindableComponent
    {
        private BindingContext _context = null;
        public BindingContext BindingContext
        {
            get => _context ?? (_context = new BindingContext());
            set => _context = value;
        }

        private ControlBindingsCollection _bindings;

        public ControlBindingsCollection DataBindings
        {
            get => _bindings ?? (_bindings = new ControlBindingsCollection(this));
            set => _bindings = value;
        }
    }
}