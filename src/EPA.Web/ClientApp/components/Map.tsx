import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Map, InfoWindow, Marker, GoogleApiWrapper } from 'google-maps-react';
import { ErrorHandlerProp, GetFetch } from './App';
import VirtualizedSelect from 'react-virtualized-select';
import 'react-select/dist/react-select.css';
import 'react-virtualized-select/styles.css';
import 'react-virtualized/styles.css';

interface DistrictInfo {
    districts: District[];
    selectDistrict: District;
    loadingDistricts: boolean;
}
interface DistrictDTO {
    name: string;
    id: number;
}

class District {
    value: number;
    label: string;
    constructor(value: number, label: string) {
        this.value = value;
        this.label = label;
    }
}

export class MapContainer extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, DistrictInfo>{
    constructor(props) {
        super(props);
        this.state = {
            districts: [],
            selectDistrict: { label: "Всі області", value: 0 },
            loadingDistricts: true
        }

    }
    componentDidMount() {
        this.fetchAllDistricts();
    }

    private fetchAllDistricts() {
        GetFetch<DistrictDTO[]>('api/ChooseSpecialties/districtsList')
            .then(data => {
                this.setState({
                    districts: data.map<District>(district => new District(district.id, district.name)),
                    loadingDistricts: false
                })
            })
            .catch(er => this.props.onError(er))
    }

    handleOnChangeDistrict = (selectDistricty) => {
        this.setState({ selectDistrict: selectDistricty })
    }

    public render() {
        return <div className="virtselect pad-for-nav col-md-offset-1  col-md-3 col-sm-offset-1 col-sm-3  col-xs-8 col-xs-offset-2"><p>Області</p>
            <VirtualizedSelect multi={false}
                options={this.state.districts}
                onChange={this.handleOnChangeDistrict}
                value={this.state.selectDistrict} >
            </VirtualizedSelect>
        </div>
    }
};
/*import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import GoogleMapReact from 'google-map-react';
import GoogleMapMarkers from 'google-map-react';
import { ErrorHandlerProp, GetFetch } from './App';
import { Component } from 'react';
const AnyReactComponent = ({ text }) => <div>{text}</div>;

const style = {
    width: '50%',
    height: '50%',
    left: '50%',
    top: '50%',
    backgroundColor: 'red',
    margin: 0,
    padding: 0,
    position: 'absolute'
    // opacity: 0.3
};

class SimpleMap extends Component {
    static defaultProps = {
        center: { lat: 59.95, lng: 30.33 },
        zoom: 11
    };

    

    public render() {
        return (
            <GoogleMapReact bootstrapURLKeys={{
                key: "AIzaSyBBg05BPuCYw2Q_A2o_m9279VV-S-zx4Ug",
            }}
                defaultCenter={{ lat: 49.05946979, lng: 33.3984375 }}
                defaultZoom={6}>
                <div style={{width: '50%', height: '50%', left: '50%', top: '50%', backgroundColor: 'red', margin: 0, padding: 0, position: 'absolute'}}>
                <GoogleMapMarkers {...{lat: 49.05946979, lng: 33.3984375 }} />
                </div>
            </GoogleMapReact>
        );
    }
}
export class MapContainer extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, {}> {
    constructor() {
        super();
        this.state = {
        }
    }
    hClick = (e) => {
        console.log(e);
    }  
    public render() {
        return <div className="text-center">
                <div style={{ width: '100%', height: '500px' }}>
                <SimpleMap/>
                </div>
            </div>
    }
}
/*
 <GoogleMapMarkers onChildClick={this.hClick}>
                <GoogleMapMarkers />
*/
