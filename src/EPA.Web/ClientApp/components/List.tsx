import * as React from 'react';
import { RouteComponentProps } from 'react-router';
//import React, { Component } from 'react';
//import { VirtualizedSelect } from 'react-virtualized-select'
import  VirtualizedSelect  from 'react-virtualized-select'
/*import { View } from 'react-native'
import SelectMultiple from 'react-native-select-multiple'
*/

import 'react-select/dist/react-select.css'
import 'react-virtualized/styles.css'
import 'react-virtualized-select/styles.css'

import 'isomorphic-fetch';
import {
    Link, NavLink, BrowserRouter as Router,
    Route
} from 'react-router-dom';

import { Tabbordion, TabPanel, TabLabel, TabContent } from 'react-tabbordion'

interface Distryct {
    name: string;
}


export default class List extends React.Component<RouteComponentProps<{}>, Distryct> {

    constructor() {
        super();
        this.state = { name: "Will" }

    }
      
    public render() {

        return <div className="col-md-offset-1  col-md-10  col-sm-10  col-xs-10 col-xs-offset-1 pagin">
            <p>{this.state.name}</p>
        </div>
    }
}
