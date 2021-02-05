import './App.css';
import React from 'react';
import { BrowserRouter, Route, Switch } from "react-router-dom";
import { Provider } from 'react-redux';
import store from './redux/stores/index';

function App() {

  const loading = (
    <div className="pt-3 text-center">
      <div className="sk-spinner sk-spinner-pulse"></div>
    </div>
  )

  // Container
  const Layout = React.lazy(() => import('./containers/appLayout'));

  // Pages
  const Login = React.lazy(() => import('./views/login/index'));

  return (
    <Provider store={store}>
      <BrowserRouter>
        <React.Suspense fallback={loading}>
            <Switch>
              <Route exact path="/login" name="Login Page" render={props => <Login {...props} />}></Route>
              <Route path="/" name="Login Page" render={props => <Layout  {...props} />}></Route>
            </Switch>
        </React.Suspense>
      </BrowserRouter> 
    </Provider>
  );
}

export default App;
