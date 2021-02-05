
import { Skeleton } from "antd";
import { Suspense } from "react";
import { Redirect, Route, Switch } from "react-router-dom";
import routes from "../routes";

function AppContent() {

    const loading = (
        <Skeleton/>
    );

    return(
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
            <Redirect from="/" to="/dashboard" />
          </Switch>
        </Suspense>
    );
};

export default AppContent;