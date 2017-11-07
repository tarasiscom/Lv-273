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

interface Specialitys {
    subjects: Subject[];
    univers: Univer[];
    districts: Distryct[];
    selectValueSub: { label: string, value: number }[];
    selectDistrict: { label: string, value: number };
}



interface Distryct {
    name: string;
    id: number;
}

interface Subject {
    id: number;
    name: string
}

interface Univer {
    name: string;
    university: string;
    address: string;
    district: string;
    site: string;
    subjects: Subject[];
}

export class ChooseSpecialityBuSubject extends React.Component<RouteComponentProps<{}>, Specialitys> {

    constructor() { super(); this.state = { subjects: [], univers: [], districts:[], selectValueSub: [], selectDistrict: { value: 0, label: "Всі" } } }

    componentDidMount() {
        this.fetchDataSubject()
    }

    fetchDataSubject() {
        fetch('api/ChooseUniversity/ChoseSpecBySub')
            .then(response => response.json() as Promise<Subject[]>)
            .then(data => {
                this.setState({ subjects: data });
            });
    }
    

    submitFiltr(selectValueSubmit,distric) {
        let resolt: number[];
        let district;
        district = "0";
        resolt = [];
        for (let i = 0; i < selectValueSubmit.length; i++) {
            resolt.push(selectValueSubmit[i].value)
        }
        fetch('api/ChooseUniversity/ChoseSpecBySublist', {
            method: 'POST',
            body: JSON.stringify(resolt),
            headers: { 'Content-Type': 'application/json' }
        }).then(response => response.json() as Promise<Univer[]>)
            .then(data => { this.setState({ univers: data }) })
    }
      
    public render() {
        
        let myListDisctict = [{ label: "Всі", value: 0 }];
        /*for (let i = 0; i < this.state.di.length; i++) {
            myList.push({ label: this.state.subjects[i].name, value: this.state.subjects[i].id })
        }*/
        let myList = [{ label: "11", value: 0 }];  
        myList.pop();
        for (let i = 0; i < this.state.subjects.length; i++)
        {
            myList.push({ label: this.state.subjects[i].name,value: this.state.subjects[i].id })
        }  


        return <div className="col-md-offset-1  col-md-10  col-sm-10  col-xs-10 col-xs-offset-1 pagin">
            <div className="navigate">
                <div className="virtselect  col-md-4 col-sm-offset-1 col-sm-4  col-xs-8 col-xs-offset-2 pagin"><p>Предмети</p>
                    <VirtualizedSelect multi={true} options={myList} onChange={(valueArray) => this.setState({ selectValueSub: valueArray })}
                        value={this.state.selectValueSub}>{console.log(this.state.selectValueSub)}</VirtualizedSelect>
                </div>
                <div className="virtselect col-md-offset-1  col-md-3 col-sm-offset-1 col-sm-3  col-xs-8 col-xs-offset-2 pagin"><p>Області</p>
                    <VirtualizedSelect  multi={false} options={myListDisctict} onChange={(selectDistrict) => this.setState({ selectDistrict } )}
                        value={this.state.selectDistrict} ></VirtualizedSelect>
                </div>
                <button className="col-md-offset-1  col-md-1 col-sm-offset-1 col-sm-2  col-xs-8 col-xs-offset-2 btn btn-primary" onClick={() => this.submitFiltr(this.state.selectValueSub, this.state.selectDistrict)}> Пошук</button>
            </div>
            <div className="col-md-offset-1  col-md-10 col-sm-offset-1 col-sm-10  C col-xs-offset-1">
                <Tabbordion animateContent="height" className="accordion" mode="toggle" role="tablist">
                    {this.state.univers.map(univer =>
                        <TabPanel >
                            <TabLabel className="glyphicon "><div className="glyphicon glyphicon-menu-down blockquote h4">Спеціальність: {univer.name} Університет:{univer.university} Область:{univer.district}</div></TabLabel>
                            <TabContent>
                                <div>
                                    <div className="col-md-6"><p>Адреса:{univer.address}</p> <p>Сайт:{univer.site}</p></div>
                                    <div className="col-md-6"><ul>Предмети:{univer.subjects.map(sub => <li> {sub.name}і </li>)} </ul></div>
                                </div>
                            </TabContent>
                        </TabPanel>
                    )}    
                </Tabbordion>

            </div>
            <div className="col-md-6 col-sm-6 col-xs-12 pad-for-footer2"></div>
        </div>
    }
}
