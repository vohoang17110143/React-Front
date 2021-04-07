import React from 'react';


const Dashboard = React.lazy(() => import('./views/dashboard/Dashboard'));
const Users = React.lazy(() => import('./views/users/Users'));
const User = React.lazy(() => import('./views/users/User'));
const Brands = React.lazy(() => import('./Component/Brands/Brand'));
const Category = React.lazy(() => import('./Component/Categories/Cate'));
const Color = React.lazy(() => import('./Component/Colors/Color'));
const Product = React.lazy(() => import('./Component/Products/Product'));
const AddProduct = React.lazy(() => import('./Component/Products/AddProduct'));


const routes = [
  { path: '/', exact: true, name: 'Home',component:Dashboard },
  { path: '/dashboard', exact: true, name: 'Dashboard', component: Dashboard },


  { path: '/dashboard/users', exact: true,  name: 'Users', component: Users },
  { path: '/dashboard/users/:id', exact: true, name: 'User Details', component: User },
  { path: '/dashboard/brands', exact: true,  name: 'Brand', component: Brands },
  { path: '/dashboard/categories', exact: true,  name: 'Categories', component: Category },
  { path: '/dashboard/color', exact: true,  name: 'Color', component: Color },
  { path: '/dashboard/product', exact: true,  name: 'Product', component: Product },
  { path: '/dashboard/addproduct', exact: true,  name: 'Add Product', component: AddProduct },
];

export default routes;
