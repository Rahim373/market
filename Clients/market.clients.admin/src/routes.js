import React from 'react';

const Dashboard = React.lazy(() => import('./views/dashboard/index'));
const Products = React.lazy(() => import('./views/catalog/products/index'));
const Categories = React.lazy(() => import('./views/catalog/categories/index'));

const routes = [
    { path: '/', exact: true, name: 'Home' },
    { path: '/dashboard', name: 'Dashboard', component: Dashboard },

    // catalog
    { path: '/catalog/products',  name: 'Products', component: Products },
    { path: '/catalog/products/add',  name: 'Products', component: Products },
    { path: '/catalog/categories', name: 'Categories', component: Categories}
];

export default routes;