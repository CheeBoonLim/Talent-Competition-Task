import React from 'react';
import { SingleInput } from '../Form/SingleInput.jsx'
import { Select } from '../Form/Select.jsx'
import { CheckBox } from '../Form/CheckBox.jsx'
import { FormErrors } from '../Form/FormErrors.jsx'

const options = [
    { key: 'm', text: 'Male', value: 'male' },
    { key: 'f', text: 'Female', value: 'female' },
]

export default class RegForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            isRemember: false,
            email: '',
            password: '',
            firstName: '',
            lastName: '',
            countryCode: '',
            mobileNumber: '',
            terms: false,
            formErrors: { firstName: '', lastName: '', mobileNumber: '', email: '', password: '' },
            emailValid: false,
            passwordValid: false,
            firstNameIsValid: false,
            lastNameIsValid: false,
            mobileIsValid: false,
            formValid: true,
            isLoading: false,
        };
        this.handleUserInput = this.handleUserInput.bind(this);
        this.validateField = this.validateField.bind(this);
        this.errorClass = this.errorClass.bind(this);
        this.isLoadingChange = this.isLoadingChange.bind(this);
        this.register = this.register.bind(this);
    };
    register() {
        this.setState({ isLoading: true });
        var registerModel = {
            firstName: this.state.firstName,
            lastName: this.state.lastName,
            countryDialCode: this.state.countryCode,
            mobilePhone: this.state.mobileNumber,
            userName: this.state.email,
            password: this.state.password,
            confirmPassword: this.state.password,
            terms: this.state.terms
        };

        $.ajax({
            url: '/Account/Register',
            type: 'POST',
            data: JSON.stringify(registerModel),
            contentType: 'application/json',
            dataType: 'json',
            success: function (response) {
                if (response.Success) {
                    window.location = '/Home/About'
                } else {

                }
            }
        });
    };
    handleUserInput(event) {
        const name = event.target.name;
        const value = event.target.type === 'checkbox' ? event.target.checked : event.target.value;
        if (event.target.type === 'checkbox') {
            console.log(name + " " + value);
            this.setState({
                [name]: value
            })
        }
        else {
            this.setState({
                [name]: value
            }, () => { this.validateField(name, value) })
        }
    };
    validateField(fieldName, value) {
        let fieldValidationErrors = this.state.formErrors;
        let emailValid = this.state.emailValid;
        let firstNameValid = this.state.firstNameIsValid;
        let lastNameValid = this.state.lastNameIsValid;
        let passwordValid = this.state.passwordValid;
        let mobileValid = this.state.mobileIsValid;
        var formValid = this.state.formValid;
        switch (fieldName) {
            case 'email':
                emailValid = value.match(/^([\w.%+-]+)@([\w-]+\.)+([\w]{2,})$/i);
                fieldValidationErrors.email = emailValid ? '' : ' is invalid';
                formValid = emailValid != null;
                break;
            case 'firstName':
                firstNameValid = value.length > 0;
                fieldValidationErrors.firstName = firstNameValid ? '' : ' is required';
                formValid = firstNameValid;
                break;
            case 'lastName':
                lastNameValid = value.length > 0;
                fieldValidationErrors.lastName = lastNameValid ? '' : ' is required';
                formValid = lastNameValid;
                break;
            case 'password':
                passwordValid = value.length >= 6;
                fieldValidationErrors.password = passwordValid ? '' : ' is too short';
                formValid = passwordValid;
                break;
            case 'mobileNumber':
                mobileValid = value.length > 0;
                fieldValidationErrors.mobileNumber = mobileValid ? '' : ' is required';
                formValid = mobileValid;
            default:
                break;
        }
        this.setState({
            formErrors: fieldValidationErrors,
            emailValid: emailValid,
            passwordValid: passwordValid,
            lastNameIsValid: lastNameValid,
            firstNameIsValid: firstNameValid,
            mobileIsValid: mobileValid,
            formValid: formValid
        }, this.validateForm);
    };
    errorClass(error) {
        return (error.length === 0 ? false : true);
    };
    isLoadingChange() {
        return this.state.isLoading == true ? 'loading' : '';
    };
    render() {
        return (
            <form className={`ui large form ${this.isLoadingChange()}`}>
                <SingleInput
                    title="First name"
                    inputType="text"
                    placeholder="First name"
                    name="firstName"
                    isError={this.errorClass(this.state.formErrors.firstName)}
                    errorMessage="Please enter first name"
                    content={this.state.firstName}
                    controlFunc={this.handleUserInput}
                />
                <SingleInput
                    title="Last name"
                    inputType="text"
                    placeholder="Last name"
                    name="lastName"
                    isError={this.errorClass(this.state.formErrors.lastName)}
                    errorMessage="Please enter last name"
                    content={this.state.lastName}
                    controlFunc={this.handleUserInput}
                />
                {/*
                <div className="field">
                    <div className="ui right labeled input">
                        <Select controlFunc={this.handleUserInput} placeholder="Please select country code" name="countryCode" options={[
                            { value: 'NZ-64', title: 'New Zealand (+64)' },
                            { value: 'AU-61', title: 'Australia (+61)' }
                        ]} />
                    </div>
                </div>
                <div className="field">
                    <SingleInput
                        title="Mobile number"
                        inputType="number"
                        placeholder="Mobile number"
                        name="mobileNumber"
                        isError={this.errorClass(this.state.formErrors.mobileNumber)}
                        errorMessage="Please enter your mobile number"
                        content={this.state.mobileNumber}
                        controlFunc={this.handleUserInput}
                    />
                </div>
                */}
                <SingleInput
                    title="Email address"
                    inputType="text"
                    placeholder="Email address"
                    name="email"
                    isError={this.errorClass(this.state.formErrors.email)}
                    errorMessage="Please enter your email"
                    content={this.state.email}
                    controlFunc={this.handleUserInput}
                />
                <SingleInput
                    title="Password"
                    inputType="password"
                    placeholder="Password"
                    name="password"
                    isError={this.errorClass(this.state.formErrors.password)}
                    errorMessage="Please enter password"
                    content={this.state.password}
                    controlFunc={this.handleUserInput}
                />
                <div className="inline field">
                    <CheckBox
                        isCheck={this.state.terms}
                        inputType="checkbox"
                        setName="terms"
                        controlFunc={this.handleUserInput}
                        selectedOptions={[
                            {
                                value: 'false',
                                title: ''
                            }]}
                        options={
                            {
                                value: 'false',
                                title: ''
                            }
                        }
                    >I agree to the <a href="/Home/TOC" target="_blank">terms and conditions</a></CheckBox>
                </div>
                <div className="field">
                    <div className="fluid ui teal button" onClick={this.register} id="submit-btn">Join</div>
                </div>
            </form>
        )
    }
}

//var RegForm = React.createClass({
//    getInitialState: function () {
//        return {
//            isRemember: false,
//            email: '',
//            password: '',
//            firstName: '',
//            lastName: '',
//            countryCode: '',
//            mobileNumber: '',
//            terms: false,
//            formErrors: { firstName: '', lastName: '', mobileNumber: '', email: '', password: '' },
//            emailValid: false,
//            passwordValid: false,
//            firstNameIsValid: false,
//            lastNameIsValid: false,
//            mobileIsValid: false,
//            formValid: true,
//            isLoading: false,
//        }
//    },
//    register: function () {
//        this.setState({ isLoading: true });
//        var registerModel = {
//            firstName: this.state.firstName,
//            lastName: this.state.lastName,
//            countryDialCode: this.state.countryCode,
//            mobilePhone: this.state.mobileNumber,
//            userName: this.state.email,
//            password: this.state.password,
//            confirmPassword: this.state.password,
//            terms: this.state.terms
//        };

//        $.ajax({
//            url: '/Account/Register',
//            type: 'POST',
//            data: JSON.stringify(registerModel),
//            contentType: 'application/json',
//            dataType: 'json',
//            success: function (response) {
//                if (response.Success) {
//                    window.location = '/Home/About'
//                } else {

//                }
//            }
//        });

//        //alert("Registered");
//    },
//    handleUserInput: function (event) {
//        console.log(event.target);
//        const name = event.target.name;
//        const value = event.target.type === 'checkbox' ? event.target.checked : event.target.value;
//        if (event.target.type === 'checkbox') {
//            console.log(name + " " + value);
//            this.setState({
//                [name]: value
//            })
//        }
//        else {
//            this.setState({
//                [name]: value
//            }, () => { this.validateField(name, value) })
//        }
//    },
//    validateField: function (fieldName, value) {
//        let fieldValidationErrors = this.state.formErrors;
//        let emailValid = this.state.emailValid;
//        let firstNameValid = this.state.firstNameIsValid;
//        let lastNameValid = this.state.lastNameIsValid;
//        let passwordValid = this.state.passwordValid;
//        let mobileValid = this.state.mobileIsValid;
//        var formValid = this.state.formValid;
//        switch (fieldName) {
//            case 'email':
//                emailValid = value.match(/^([\w.%+-]+)@([\w-]+\.)+([\w]{2,})$/i);
//                fieldValidationErrors.email = emailValid ? '' : ' is invalid';
//                formValid = emailValid != null;
//                break;
//            case 'firstName':
//                firstNameIsValid = value.length > 0;
//                fieldValidationErrors.firstName = firstNameIsValid ? '' : ' is required';
//                formValid = firstNameIsValid;
//                break;
//            case 'lastName':
//                lastNameIsValid = value.length > 0;
//                fieldValidationErrors.lastName = lastNameIsValid ? '' : ' is required';
//                formValid = lastNameIsValid;
//                break;
//            case 'password':
//                passwordValid = value.length >= 6;
//                fieldValidationErrors.password = passwordValid ? '' : ' is too short';
//                formValid = passwordValid;
//                break;
//            case 'mobileNumber':
//                mobileValid = value.length > 0;
//                fieldValidationErrors.mobileNumber = mobileValid ? '' : ' is required';
//                formValid = mobileValid;
//            default:
//                break;
//        }
//        this.setState({
//            formErrors: fieldValidationErrors,
//            emailValid: emailValid,
//            passwordValid: passwordValid,
//            lastNameIsValid: lastNameValid,
//            firstNameIsValid: firstNameValid,
//            mobileIsValid: mobileValid,
//            formValid: formValid
//        }, this.validateForm);
//    },
//    errorClass: function (error) {
//        return (error.length === 0 ? false : true);
//    },
//    isLoadingChange: function () {
//        return this.state.isLoading == true ? 'loading' : '';
//    },
//    render: function () {
//        return (
//            <form className={`ui large form ${this.isLoadingChange()}`}>
//                <SingleInput
//                    title="First name"
//                    inputType="text"
//                    placeholder="First name"
//                    name="firstName"
//                    isError={this.errorClass(this.state.formErrors.firstName)}
//                    errorMessage="Please enter first name"
//                    content={this.state.firstName}
//                    controlFunc={this.handleUserInput}
//                />
//                <SingleInput
//                    title="Last name"
//                    inputType="text"
//                    placeholder="Last name"
//                    name="lastName"
//                    isError={this.errorClass(this.state.formErrors.lastName)}
//                    errorMessage="Please enter last name"
//                    content={this.state.lastName}
//                    controlFunc={this.handleUserInput}
//                />
//                {/*
//                <div className="field">
//                    <div className="ui right labeled input">
//                        <Select controlFunc={this.handleUserInput} placeholder="Please select country code" name="countryCode" options={[
//                            { value: 'NZ-64', title: 'New Zealand (+64)' },
//                            { value: 'AU-61', title: 'Australia (+61)' }
//                        ]} />
//                    </div>
//                </div>
//                <div className="field">
//                    <SingleInput
//                        title="Mobile number"
//                        inputType="number"
//                        placeholder="Mobile number"
//                        name="mobileNumber"
//                        isError={this.errorClass(this.state.formErrors.mobileNumber)}
//                        errorMessage="Please enter your mobile number"
//                        content={this.state.mobileNumber}
//                        controlFunc={this.handleUserInput}
//                    />
//                </div>
//                */}
//                <SingleInput
//                    title="Email address"
//                    inputType="text"
//                    placeholder="Email address"
//                    name="email"
//                    isError={this.errorClass(this.state.formErrors.email)}
//                    errorMessage="Please enter your email"
//                    content={this.state.email}
//                    controlFunc={this.handleUserInput}
//                />
//                <SingleInput
//                    title="Password"
//                    inputType="password"
//                    placeholder="Password"
//                    name="password"
//                    isError={this.errorClass(this.state.formErrors.password)}
//                    errorMessage="Please enter password"
//                    content={this.state.password}
//                    controlFunc={this.handleUserInput}
//                />
//                <div className="inline field">
//                    <CheckBox
//                        isCheck={this.state.terms}
//                        inputType="checkbox"
//                        setName="terms"
//                        controlFunc={this.handleUserInput}
//                        selectedOptions={[
//                            {
//                                value: 'false',
//                                title: ''
//                            }]}
//                        options={
//                            {
//                                value: 'false',
//                                title: ''
//                            }
//                        }
//                    >I agree to the <a href="/Home/TOC" target="_blank">terms and conditions</a></CheckBox>
//                </div>
//                <div className="field">
//                    <div className="fluid ui teal button" onClick={this.register} id="submit-btn">Join</div>
//                </div>
//            </form>
//        )
//    }
//})
