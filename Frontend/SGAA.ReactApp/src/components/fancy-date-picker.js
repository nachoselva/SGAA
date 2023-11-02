import { TextField } from '@mui/material';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import React from 'react';

export const FancyDatePicker = (props) =>
(<DatePicker
  variant="filled"
  value={props.value}
  label={props.label}
  name={props.name}
  onChange={props.onChange}
  renderInput={(params) => {
    params.error = !!(props.touched && props.error);
    return <TextField
      name={props.name}
      helperText={props.touched && props.error}
      variant="filled"
      fullWidth
      onBlur={props.onBlur}
      {...params} />
  }
  }
/>);