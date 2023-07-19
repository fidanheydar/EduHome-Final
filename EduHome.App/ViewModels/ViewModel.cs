using EduHome.Core.Entities;

namespace EduHome.App.ViewModels
{
    public class ViewModel
    {
        public ICollection<Slider>? sliders { get; set; }
        public ICollection<Notice>? notices { get; set; }

        
    }
}
