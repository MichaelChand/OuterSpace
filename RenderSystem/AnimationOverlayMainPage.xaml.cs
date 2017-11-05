using OuterSpace.AnimationSystems.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RenderSystem
{
    /// <summary>
    /// Interaction logic for AnimationOverlayMainPage.xaml
    /// </summary>
    public partial class AnimationOverlayMainPage : Page
    {
        public IAnimation AnimationToRender { get; set; }

        public AnimationOverlayMainPage()
        {
            InitializeComponent();
        }

        public void LoadAnimation()
        {
            AnimationToRender.Start();
        }

        public void Render()
        {

        }
    }
}
