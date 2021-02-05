import { Skeleton } from 'antd';
import React, { Suspense, useEffect, useState } from 'react';
import { Route, Switch, useLocation } from 'react-router-dom';

function Categories() {

    const loading = (
        <Skeleton/>
    );

    const location = useLocation();

    const Categories = React.lazy(() => import('./list'));
    const AddCategory = React.lazy(() => import('./add'));

    const routes = [
        {path: `/catalog/categories`, exact: true, name: 'list', name: 'List', component: Categories },
        {path: `/catalog/categories/add`, exact: true, name: 'add', name: 'Add Category', component: AddCategory }
    ];

    return (
        <Suspense fallback={loading}>
        <Switch>
          {routes.map((route, idx) => {
            return route.component && (
              <Route
                key={idx}
                path={route.path}
                exact={route.exact}
                name={route.name}
                render={props => (
                  <route.component {...props} />
                )} />
            )
          })}
        </Switch>
      </Suspense>
    );
};

export default Categories;