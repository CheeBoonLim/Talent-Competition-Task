/* Availability section */
import React from 'react';

export default class Availability extends React.Component {
    constructor(props) {
        super(props);
        //initialize functions
        this.getAvailability = this.getAvailability.bind(this);
        this.onDropdownSelected = this.onDropdownSelected.bind(this);
        //set default state
        this.state = {
            availabilityType: '',
            availableHours: '',
            earnTarget: ''
        }
    };

    getAvailability() {
        var self = this;
        $.ajax({
            url: '/Home/GetAvailabilityType',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.availability) {
                    self.setState({
                        availabilityType: res.availability,
                        availableHours: res.availableHours,
                        earnTarget: res.earnTarget
                    })
                }
            }
        })
    };
   
    onDropdownSelected(props) {
        if (props.target.name == "availabilityType") {
            this.setState({
                availabilityType: props.target.value
            })
            //console.log("availabilityType : ",  this.state.availabilityType);
        }
        if (props.target.name == "availableHours") {
            this.setState({
                availableHours: props.target.value
            })
            //console.log("availableHours : ", this.state.availableHours);
        }
        if (props.target.name == "earnTarget") {
            this.setState({
                earnTarget: props.target.value
            })
            //console.log("earnTarget : ", this.state.earnTarget);
        }
    };

    componentDidMount() {
        this.getAvailability();
    };

    render() {
        return (
            <div className="row">
                <div className="four wide column">
                    <h3 className="alt" data-hint="">Availability</h3>
                    <div className="tooltip">You can pick your work type and availability here.</div>
                </div>
                <div className="four wide field">
                    <select className="ui right labeled dropdown" value={this.state.availabilityType} name="availabilityType" onChange={this.onDropdownSelected} placeholder="I want to work">
                        <option value="0">Part time</option>
                        <option value="1">Full time</option>
                    </select>
                </div>
                <div className="four wide field">
                    <select className="ui dropdown" value={this.state.availableHours} name="availableHours" onChange={this.onDropdownSelected} placeholder ="I can work">
                        <option value="0">Less than 30hours a week</option>
                        <option value="1">More than 30hours a week</option>
                        <option value="2">As needed</option>
                    </select>
                </div>
                <div className="four wide field">
                    <select className="ui dropdown" value={this.state.earnTarget} name="earnTarget" onChange={this.onDropdownSelected} placeholder = "I would like to earn">
                        <option value="0">Less than $500 per month</option>
                        <option value="1">Between $500 and $1000 per month</option>
                        <option value="2">More than $1000 per month</option>
                    </select>
                </div>
            </div>
        )
    }
}

