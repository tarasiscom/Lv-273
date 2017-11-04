import * as React from 'react';
import { RouteComponentProps } from 'react-router';
//import React, { Component } from 'react';
//import { VirtualizedSelect } from 'react-virtualized-select'
import VirtualizedSelect from 'react-virtualized-select'
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



interface ListSub {
    subjects: Subject[];
    selectValue: any;
}

interface Subject {
    id: number;
    name: string;
    
}

export class ChooseSpecialityBuSubject extends React.Component<RouteComponentProps<{}>, ListSub> {

    constructor() { super(); this.state = { subjects: [], selectValue:0 } }

    componentDidMount() {
        this.fetchData()
    }
    //onPrimaryUpdated(){}
    fetchData()
    {
        fetch('api/ChooseUniversity/ChoseSpecBySub')
            .then(response => response.json() as Promise<Subject[]>)
            .then(data => {
                this.setState({ subjects: data });
            });
    }
    Ope() { }

    public render() {
        const options = [
            { label: "One", value: 1 },
            { label: "Two", value: 2 },
            { label: "Three", value: 3 }
            // And so on... 
        ]
        return <div>

            <VirtualizedSelect multi="multi" options={options} onChange={(selectValue) => this.setState({ selectValue })}
                value={this.state.selectValue}>la</VirtualizedSelect>
            {this.state.subjects.map((sub, index) =>
                <div>
                    <p> {sub.id} {sub.name}</p>
                </div>
                )
            }
            
        </div>
    }
}
