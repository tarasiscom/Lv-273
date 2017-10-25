import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

class Parent extends React.Component {
  constructor(props){
    super(props);
    this.state = {
      show: false
    };
  }
  updateState = () => {
      this.setState({
          show: !this.state.show
      });
  }
  render() {
    return (
        <Child updateState={this.updateState} />
    );
  }
}

class Child extends React.Component {
  handleClick = () => {
      this.props.updateState();
  }
  render() {
    return (
        <button onClick={this.handleClick}>Test</button>
    )
  };
}