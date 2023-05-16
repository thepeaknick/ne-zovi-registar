import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Registar',
    url: '/registry',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Korisnici',
        url: '/registry/users'
      },
      {
        name: 'Obveznici',
        url: '/registry/regusers'
      },
    ]
  },
  {
    divider: true,
  },
  {
    name: 'Admin panel',
    url: '/admin',
    iconComponent: { name: 'cil-chart-pie' }
  },
  {
    divider: true,
  },
  {
    name: 'Podešavanja',
    url: '/settings',
    iconComponent: { name: 'cil-chart-pie' }
  },
  {
    divider: true,
  },
  {
    name: 'Pomoć',
    url: '/help',
    iconComponent: { name: 'cil-chart-pie' }
  },
  {
    divider: true,
  },
  {
    name: 'Kontaktirajte nas',
    url: '/contact',
    iconComponent: { name: 'cil-chart-pie' }
  },
];
