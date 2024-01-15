using InterfaceAAD.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAAD.ViewModels
{
    public class PropertyListViewModel : BaseViewModel
    {
        private DataTable _properties = null;

        public DataTable Properties
        { 
            get { return _properties; }
            set
            {
                if (_properties != value)
                {
                    _properties = value;
                    OnPropertyChanged(nameof(Properties));
                }
            }
        }



        public PropertyListViewModel()
        {
            LoadProperties();
        }

        private async void LoadProperties()
        {
            PropertyRepository propertyRepository = new PropertyRepository();
            Properties = await propertyRepository.GetAllPropertiesAsDataTable();
        }



    }
}
