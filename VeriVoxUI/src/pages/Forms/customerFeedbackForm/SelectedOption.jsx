import React from 'react'
import ShortText from './ShortText1'
import NumberInput from './NumberInput'
import BigText from './BigText'
import Ratings from './Ratings'
import Dropdown from './Dropdown'
import RadioButtons from './RadioButtons'
import TypeAhead from './TypeAhead'
import CheckBox from './CheckBox'

const SelectedOption = ({answerType, option }) => {
    switch (answerType){
        case "ShortText":
            return <ShortText/>;
        case 'NumberInput':
            return <NumberInput/>;
        case 'BigText':
            return <BigText/>;
        case 'Ratings':
            return <Ratings/>;
        case 'Dropdown':
            return <Dropdown option={option}/>;
        case 'RadioButtons':
            return <RadioButtons option={option}/>;
        case 'TypeAhead':
            return <TypeAhead option={option}/>;
        case 'CheckBox':
            return <CheckBox option={option}/>;
        default:
            return null;
        }
}

export default SelectedOption
