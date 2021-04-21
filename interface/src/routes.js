import React from 'react';
import { BrowserRouter, Route, Switch} from 'react-router-dom';


import Logon from './Pages/Logon';
import Home from './Pages/Home';

export default function Routes()
{
    return (
        <BrowserRouter>
            <Switch>
                <Route path="/" exact component={Logon} />
                <Route path="/home" component={Home} />
            </Switch>
        </BrowserRouter>
    )
}