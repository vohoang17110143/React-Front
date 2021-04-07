import React from 'react'
import CIcon from '@coreui/icons-react'

const _nav =  [
  {
    _tag: 'CSidebarNavItem',
    name: 'Dashboard',
    to: '/dashboard',
    icon: <CIcon name="cil-speedometer" customClasses="c-sidebar-nav-icon"/>,
    badge: {
      color: 'info',
      text: 'NEW',
    }
  },
  // {
  //   _tag: 'CSidebarNavTitle',
  //   _children: ['Theme']
  // },
  // {
  //   _tag: 'CSidebarNavItem',
  //   name: 'Colors',
  //   to: '/theme/colors',
  //   icon: 'cil-drop',
  // },
  // {
  //   _tag: 'CSidebarNavItem',
  //   name: 'Typography',
  //   to: '/theme/typography',
  //   icon: 'cil-pencil',
  // },
  {
    _tag: 'CSidebarNavTitle',
    _children: ['Components']
  },
  {
    _tag: 'CSidebarNavDropdown',
    name: 'Products',
    route: '/dashboard/base',
    icon: 'cil-puzzle',
    _children: [
      {
        _tag: 'CSidebarNavItem',
        name: 'Product',
        to: '/dashboard/product',
        icon: 'cil-star'
      },
      {
        _tag: 'CSidebarNavItem',
        name: 'Color',
        to: '/dashboard/color',
        icon: 'cil-drop'
      },
      {
        _tag: 'CSidebarNavItem',
        name: 'Brand',
        to: '/dashboard/brands',
        icon: 'cil-tags'
      },
      {
        _tag: 'CSidebarNavItem',
        name: 'Categories',
        to: '/dashboard/categories',
        icon: 'cil-layers'
      },
      {
        _tag: 'CSidebarNavItem',
        name: 'Size',
        to: '/dashboard/Size',
        icon: 'cil-pencil'
      },
    ],
  }
]

export default _nav
