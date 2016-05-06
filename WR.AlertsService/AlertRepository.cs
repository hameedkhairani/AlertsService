using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WR.AlertsService
{
    public class AlertRepository: IAlertRepository
    {
        private readonly List<Alert> _alerts = new List<Alert>();

        public void Add(Alert alert)
        {
            _alerts.Add(alert);
        }

        public List<Alert> GetAll()
        {
            return _alerts;
        }
    }

    public interface IAlertRepository
    {
        void Add(Alert alert);
        List<Alert> GetAll();
    }
}
