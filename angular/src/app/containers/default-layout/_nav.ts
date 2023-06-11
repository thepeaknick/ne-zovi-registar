import { INavData } from '@coreui/angular';

export interface ICustomNavData extends INavData {
  roles?: string[];
  children?: ICustomNavData[];
}

export const navItems: ICustomNavData[] = [
  {
    name: 'Početna',
    url: '/admin',
    iconComponent: { name: 'cil-user' }
  },
  {
    divider: true,
  },
  {
    name: 'Registar',
    url: '/registry',
    iconComponent: { name: 'cil-spreadsheet' },
    children: [
      {
        name: 'Korisnici',
        url: '/registry/users',
        iconComponent: { name: 'cil-group' },
      },
      {
        name: 'Obveznici',
        url: '/registry/regusers',
        iconComponent: { name: 'cil-briefcase' },
        roles: ['Admin']
      },
      {
        name: 'Trgovci',
        url: '/registry/merchants',
        iconComponent: { name: 'cil-cart' },
        roles: ['Admin']
      },
    ]
  },
  {
    divider: true,
  },
  {
    name: 'Podešavanja',
    url: '/settings',
    iconComponent: { name: 'cil-settings' }
  },
  {
    divider: true,
  },
  {
    name: 'Pomoć',
    url: '/help',
    iconComponent: { name: 'cil-puzzle' }
  },
  {
    divider: true,
  },
  {
    name: 'Kontaktirajte nas',
    url: '/contact',
    iconComponent: { name: 'cil-envelope-closed' }
  },
  {
    name: 'Sign out',
    url: '/pages/signout',
    iconComponent: { name: 'cil-exit-to-app' }
  },
];
