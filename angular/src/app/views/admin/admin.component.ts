import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import * as XLSX from 'xlsx';

import {
  cilArrowTop,
  cilArrowBottom,
  cilOptions,
  cilChartPie,
  cilArrowRight,
} from '@coreui/icons';
import { getStyle } from '@coreui/utils';
import {
  RegUserDto,
  RoleType,
  UserDto,
  UserDtoPagedList,
} from 'src/app/domain/model/schemas';
import { RegUserService } from 'src/app/domain/services/reguser.service';
import { UserService } from 'src/app/domain/services/user.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss'],
})
export class AdminComponent implements OnInit {
  constructor(
    private userService: UserService,
    private regUserService: RegUserService
  ) {}

  @Input() usersData!: UserDtoPagedList;
  @Input() users: UserDto[] = [];
  @Input() merchants: RegUserDto[] = [];
  @Input() regUsers: RegUserDto[] = [];

  @Input() mtsUsers: UserDto[] = [];
  @Input() a1Users: UserDto[] = [];
  @Input() yettelUsers: UserDto[] = [];

  fileName = 'Statistika.xlsx';

  icons = {
    cilOptions,
    cilArrowTop,
    cilArrowBottom,
    cilChartPie,
    cilArrowRight,
  };

  data1: any = {};
  data2: any = {};
  data3: any = {};
  options1: any = {};
  options2: any = {};
  options3: any = {};

  options: any = {};

  labels = [
    'Januar',
    'Februar',
    'Mart',
    'April',
    'Maj',
    'Jun',
    'Jul',
    'Avgust',
    'Septembar',
    'Octobar',
    'Novembar',
    'Decembar',
  ];

  dataLineChart = {
    labels: this.labels,
    datasets: [
      {
        label: 'Aktivni u registru',
        backgroundColor: 'rgba(220, 220, 220, 0.2)',
        borderColor: '#FF0000',
        pointBackgroundColor: '#FF0000',
        pointBorderColor: '#fff',
        data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
        tension: 0.5,
      },
      {
        label: 'Neaktivni u registru',
        backgroundColor: 'rgba(151, 187, 205, 0.2)',
        borderColor: '#3F6AA3',
        pointBackgroundColor: '#3F6AA3',
        pointBorderColor: '#fff',
        data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
        tension: 0.5,
      },
    ],
  };

  dataPieChart = {
    labels: ['Yettel', 'mts', 'A1'],
    datasets: [
      {
        backgroundColor: ['#133F85', '#C0CBDB', '#FF0000'],
        data: [0, 0, 0],
      },
    ],
  };

  datasets = [
    {
      label: 'Aktivni u registru',
      backgroundColor: 'transparent',
      borderColor: '#3F6AA3',
      pointBackgroundColor: '#3F6AA3',
      pointHoverBorderColor: getStyle('--cui-primary'),
      data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    },
  ];

  datasets2 = [
    {
      label: 'Aktivni u registru',
      backgroundColor: 'transparent',
      borderColor: '#3F6AA3',
      pointBackgroundColor: '#3F6AA3',
      pointHoverBorderColor: getStyle('--cui-primary'),
      data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    },
  ];

  datasets3 = [
    {
      label: 'Aktivni u registru',
      backgroundColor: 'transparent',
      borderColor: '#3F6AA3',
      pointBackgroundColor: '#3F6AA3',
      pointHoverBorderColor: getStyle('--cui-primary'),
      data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
    },
  ];

  optionsDefault = {
    plugins: {
      legend: {
        display: false,
      },
    },
    maintainAspectRatio: true,
    scales: {
      x: {
        grid: {
          display: false,
          drawBorder: false,
        },
        ticks: {
          display: false,
        },
      },
      y: {
        display: true,
        grid: {
          display: false,
        },
        ticks: {
          display: true,
        },
      },
    },
    elements: {
      line: {
        borderWidth: 1,
        tension: 0.4,
      },
      point: {
        radius: 4,
        hitRadius: 10,
        hoverRadius: 4,
      },
    },
  };

  handleChartRef($chartRef: any) {
    if ($chartRef) {
      setTimeout(() => {
        $chartRef?.update();
      }, 3000);
    }
  }

  calculatePerOperatorPerMonth(
    users: UserDto[],
    perMonthActive: Array<number>,
    perMonthInactive: Array<number>,
    operatorName: string
  ) {
    users.forEach((user) => {
      const formattedDate = new Date(user.registeredOn);
      const month = formattedDate.getMonth();
      const year = formattedDate.getMonth();
      if (user.operator == operatorName || operatorName == 'all') {
        if (user.active == true) {
          perMonthActive[month] = perMonthActive[month] + 1;
        } else {
          perMonthInactive[month] = perMonthInactive[month] + 1;
        }
      }
    });
  }

  ngOnInit(): void {
    this.data1 = {
      labels: this.labels.slice(0, 11),
      datasets: this.datasets,
    };
    this.data2 = {
      labels: this.labels.slice(0, 11),
      datasets: this.datasets2,
    };
    this.data3 = {
      labels: this.labels.slice(0, 11),
      datasets: this.datasets3,
    };
    this.options1 = this.optionsDefault;
    this.options2 = this.optionsDefault;
    this.options3 = this.optionsDefault;
    let after: Date = new Date();
    after.setMonth(3);

    this.userService.allUsers(after).subscribe({
      next: (usersData: UserDtoPagedList) => {
        this.usersData =
          usersData instanceof HttpErrorResponse
            ? ({} as UserDtoPagedList)
            : usersData;

        this.users = this.usersData.items;
        //count per operator active
        const items = this.usersData.items;
        this.mtsUsers = items.filter(
          (item) =>
            item.operator == 'TELEKOM SRBIJA AD BEOGRAD' && item.active == true
        );
        this.a1Users = items.filter(
          (item) => item.operator == 'A1 SRBIJA' && item.active == true
        );
        this.yettelUsers = items.filter(
          (item) => item.operator == 'YETTEL D.O.O.' && item.active == true
        );

        //get data for last 6 months
        var perMonthActive = new Array<number>(12).fill(0);
        var perMonthInactive = new Array<number>(12).fill(0);
        this.calculatePerOperatorPerMonth(
          this.yettelUsers,
          perMonthActive,
          perMonthInactive,
          'YETTEL D.O.O.'
        );

        this.datasets[0].data = perMonthActive;
        this.data1 = {
          labels: this.labels.slice(0, 11),
          datasets: this.datasets,
        };

        var perMonthActive1 = new Array<number>(12).fill(0);
        var perMonthInactive1 = new Array<number>(12).fill(0);
        this.calculatePerOperatorPerMonth(
          this.mtsUsers,
          perMonthActive1,
          perMonthInactive1,
          'TELEKOM SRBIJA AD BEOGRAD'
        );

        this.datasets2[0].data = perMonthActive1;
        this.data2 = {
          labels: this.labels.slice(0, 11),
          datasets: this.datasets2,
        };

        var perMonthActive2 = new Array<number>(12).fill(0);
        var perMonthInactive2 = new Array<number>(12).fill(0);
        this.calculatePerOperatorPerMonth(
          this.a1Users,
          perMonthActive2,
          perMonthInactive2,
          'A1 SRBIJA'
        );
        this.datasets3[0].data = perMonthActive2;
        this.data3 = {
          labels: this.labels.slice(0, 11),
          datasets: this.datasets3,
        };

        // pie chart
        this.dataPieChart = {
          labels: ['Yettel', 'mts', 'A1'],
          datasets: [
            {
              backgroundColor: ['#133F85', '#C0CBDB', '#FF0000'],
              data: [
                this.yettelUsers.length,
                this.mtsUsers.length,
                this.a1Users.length,
              ],
            },
          ],
        };

        // bottom table
        var perMonthActive3 = new Array<number>(12).fill(0);
        var perMonthInactive3 = new Array<number>(12).fill(0);
        this.calculatePerOperatorPerMonth(
          this.users,
          perMonthActive3,
          perMonthInactive3,
          'all'
        );

        this.dataLineChart = {
          labels: this.labels,
          datasets: [
            {
              label: 'Aktivni u registru',
              backgroundColor: 'rgba(220, 220, 220, 0.2)',
              borderColor: '#FF0000',
              pointBackgroundColor: '#FF0000',
              pointBorderColor: '#fff',
              data: perMonthActive3,
              tension: 0.5,
            },
            {
              label: 'Neaktivni u registru',
              backgroundColor: 'rgba(151, 187, 205, 0.2)',
              borderColor: '#3F6AA3',
              pointBackgroundColor: '#3F6AA3',
              pointBorderColor: '#fff',
              data: perMonthInactive3,
              tension: 0.5,
            },
          ],
        };
      },
    });

    this.regUserService.getRegUsers(RoleType.Obveznik).subscribe({
      next: (regUsers: RegUserDto[]) =>
        (this.regUsers = regUsers instanceof HttpErrorResponse ? [] : regUsers),
    });

    this.regUserService.getRegUsers(RoleType.Trgovac).subscribe({
      next: (merchants: RegUserDto[]) =>
        (this.merchants =
          merchants instanceof HttpErrorResponse ? [] : merchants),
    });
  }

  exportExcel(): void {
    // Set column widths
    const columnWidths = [
      { wch: 30 }, // Column A width
      { wch: 20 }, // Column B width
      { wch: 20 }, // Column C width
      { wch: 20 }, // Column D width
    ];
    const wb: XLSX.WorkBook = XLSX.utils.book_new();

    var perMonthActive = new Array<number>(12).fill(0);
    var perMonthInactive = new Array<number>(12).fill(0);
    this.calculatePerOperatorPerMonth(
      this.users,
      perMonthActive,
      perMonthInactive,
      'all'
    );

    var ws_header = [['Aktivni korisnici po mesecima']];
    var statsSheet = XLSX.utils.aoa_to_sheet(ws_header);
    var ws_data = [this.labels, perMonthActive];
    var statsSheet = XLSX.utils.sheet_add_aoa(statsSheet, ws_data, {
      origin: -1,
    });

    var ws_header2 = [['Neaktivni korisnici po mesecima']];
    var statsSheet = XLSX.utils.sheet_add_aoa(statsSheet, ws_header2, {
      origin: 'A6',
    });
    var ws_data2 = [this.labels, perMonthInactive];
    var statsSheet = XLSX.utils.sheet_add_aoa(statsSheet, ws_data2, {
      origin: -1,
    });

    let merchantsData = this.merchants.map(
      ({ guidId, id, createdOn, ...item }) => {
        const formattedDate = new Date(createdOn).toLocaleDateString();
        return {
          'Ime firme': item.name,
          PIB: item.taxNumber,
          MB: item.regNumber,
          // Adresa: item.address,
          'Datum upisa': formattedDate,
        };
      }
    );
    console.log(merchantsData);

    const merchantsSheet: XLSX.WorkSheet =
      XLSX.utils.json_to_sheet(merchantsData);

    // Update column widths in the worksheet
    columnWidths.forEach((width, colIndex) => {
      merchantsSheet['!cols'] = merchantsSheet['!cols'] || [];
      merchantsSheet['!cols'][colIndex] = { wch: width.wch };
    });

    // Update first row height
    merchantsSheet['!rows'] = merchantsSheet['!rows'] || [];
    merchantsSheet['!rows'][0] = { hpx: 30 };

    let data = this.regUsers.map(({ guidId, id, createdOn, ...item }) => {
      const formattedDate = new Date(createdOn).toLocaleDateString();
      return {
        'Ime firme': item.name,
        PIB: item.taxNumber,
        MB: item.regNumber,
        // Adresa: item.address,
        'Datum upisa': formattedDate,
      };
    });
    const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(data);

    // Update column widths in the worksheet
    columnWidths.forEach((width, colIndex) => {
      ws['!cols'] = ws['!cols'] || [];
      ws['!cols'][colIndex] = { wch: width.wch };
    });

    // Update first row height
    ws['!rows'] = ws['!rows'] || [];
    ws['!rows'][0] = { hpx: 30 };

    /* generate workbook and add the worksheet */

    XLSX.utils.book_append_sheet(wb, ws, 'Operatori');
    XLSX.utils.book_append_sheet(wb, merchantsSheet, 'Trgovci');
    XLSX.utils.book_append_sheet(wb, statsSheet, 'Statistika');

    /* save to file */
    XLSX.writeFile(wb, this.fileName);
  }
}
