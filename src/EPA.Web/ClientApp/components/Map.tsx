import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Map, InfoWindow, Marker, GoogleApiWrapper } from 'google-maps-react';
import { ErrorHandlerProp, GetFetch } from './App';

interface StateType {
    showingInfoWindow: boolean;
    activeMarker: Marker;
    selectedPlace: any;
}
export class MapContainer extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, StateType>{
    constructor(props) {
        super(props);
        this.state = {
            showingInfoWindow: false,
            activeMarker: {},
            selectedPlace: {},
        }

        // binding this to event-handler functions
        this.onMarkerClick = this.onMarkerClick.bind(this);
        this.onMapClicked = this.onMapClicked.bind(this);
    }

    onMarkerClick=(props, marker, e)=> {
        this.setState({
            selectedPlace: props,
            activeMarker: marker,
            showingInfoWindow: true
        });
    }
 
    onMapClicked=(props) => {
        if (this.state.showingInfoWindow) {
            this.setState({
                showingInfoWindow: false,
                activeMarker: null
            })
        }
    }
 
    public render(){
        return <div>
            <Map //google={}
                mapCenter={{ lat: 32.4331976, lng: -97.7828788 }}
                onClick={this.onMapClicked}>
                <Marker onClick={this.onMarkerClick}
                    name={'Current location'} />

                <InfoWindow
                    marker={this.state.activeMarker}
                    visible={this.state.showingInfoWindow}>
                    <div>
                        <h1>{this.state.selectedPlace.name}</h1>
                    </div>
                </InfoWindow>
            </Map>
            </div>
    }
};

export default GoogleApiWrapper({
    apiKey: ("AIzaSyDzFpCAsTslZLDZ7kixtlV258plw3IHKH8")
})(MapContainer)

/*

//export class Map extends React.Component { }

export default GoogleApiWrapper({
    apiKey: (YOUR_GOOGLE_API_KEY_GOES_HERE)
})(Map)

import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import GoogleMapReact from 'google-map-react';
import { ErrorHandlerProp, GetFetch } from './App';

const AnyReactComponent = ({ text }) => <div>{text}</div>;


interface MapData
{
    center: Center;
    zoom: number;
}

interface Center
{
    lat: number;
    lng: number;
}

export class Map extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, MapData> { 
    constructor(props) {
        super(props);
        this.state = {
            center: { lat: 59.95, lng: 30.33 },
            zoom: 11
        }
    }


    public render() {
        return <div>
            <GoogleMapReact
                defaultCenter={this.state.center}
                defaultZoom={this.state.zoom}>
                
            lat={this.state.center.lat}
            lng={this.state.center.lng}
            text={'Kreyser Avrora'}
                
            </GoogleMapReact>
        </div>
    }
}*/