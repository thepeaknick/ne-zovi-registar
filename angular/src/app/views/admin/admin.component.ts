import { Component, OnInit } from '@angular/core';

import { cilArrowTop, cilArrowBottom, cilOptions } from '@coreui/icons';
import { getStyle } from '@coreui/utils';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})

export class AdminComponent implements OnInit {

    icons = { cilOptions, cilArrowTop, cilArrowBottom };
  
    data: any = {};
    options: any = {};

    dataLineChart = {
      labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
      datasets: [
        {
          label: 'My First dataset',
          backgroundColor: 'rgba(220, 220, 220, 0.2)',
          borderColor: '#FF0000',
          pointBackgroundColor: '#FF0000',
          pointBorderColor: '#fff',
          data: [40, 20, 12, 39, 10, 80, 40]
        },
        {
          label: 'My Second dataset',
          backgroundColor: 'rgba(151, 187, 205, 0.2)',
          borderColor: '#3F6AA3',
          pointBackgroundColor: '#3F6AA3',
          pointBorderColor: '#fff',
          data: [50, 12, 28, 29, 7, 25, 60]
        }
      ]
    };

    dataPieChart = {
      labels: ['Yettel', 'mts', 'A1'],
      datasets: [{
        backgroundColor: ['#133F85', '#C0CBDB', '#FF0000'],
        data: [80, 20, 50]
      }]
    };
  
    labels = [
      'January',
      'February',
      'March',
      'April',
      'May',
      'June',
      'July',
      'August',
      'September',
      'October',
      'November',
      'December',
      'January',
      'February',
      'March',
      'April'
    ];
  
    datasets = [
      {
        label: 'My First dataset',
        backgroundColor: 'transparent',
        borderColor: '#3F6AA3',
        pointBackgroundColor: '#3F6AA3',
        pointHoverBorderColor: getStyle('--cui-primary'),
        data: [65, 59, 84, 84, 51, 55, 40]
      },
    ];
  
    optionsDefault = {
      plugins: {
        legend: {
          display: false
        }
      },
      maintainAspectRatio: true,
      scales: {
        x: {
          grid: {
            display: false,
            drawBorder: false
          },
          ticks: {
            display: false
          }
        },
        y: {
          min: 30,
          max: 89,
          display: false,
          grid: {
            display: false
          },
          ticks: {
            display: false
          }
        }
      },
      elements: {
        line: {
          borderWidth: 1,
          tension: 0.4
        },
        point: {
          radius: 4,
          hitRadius: 10,
          hoverRadius: 4
        }
      }
    };

    handleChartRef($chartRef: any) {
      if ($chartRef) {
        console.log('handleChartRef', $chartRef);
        this.dataLineChart.labels.push('August');
        this.dataLineChart.datasets[0].data.push(60);
        this.dataLineChart.datasets[1].data.push(20);
        setTimeout(() => {
          $chartRef?.update();
        }, 3000);
      }
    }
  
    ngOnInit(): void {
      this.data = {
        labels: this.labels.slice(0, 7),
        datasets: this.datasets
      };
      this.options = this.optionsDefault;
    }
  }
