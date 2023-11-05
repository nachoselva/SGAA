import ArrowDownOnSquareIcon from '@heroicons/react/24/solid/ArrowDownOnSquareIcon';
import ArrowUpOnSquareIcon from '@heroicons/react/24/solid/ArrowUpOnSquareIcon';
import TrashIcon from '@heroicons/react/24/solid/TrashIcon';
import { Box, Button, Input, Link, SvgIcon, TextField } from '@mui/material';
import React from 'react';
import { convertFileToBase64 } from '/src/utils/file-base64-converter';

export const FancyFilePicker = (props) => {
  const { touched, error, name, label, onChange, onBlur, file, readOnly } = props;
  const currentFile = JSON.parse(file);

  const handleChange = async (file) => {
    if (file) {
      const result = await convertFileToBase64(file);
      if (result?.base64 !== currentFile?.base64 && result?.size < 200) {
        onChange(JSON.stringify(result));
      }
    }
    else {
      onChange(null);
    }
  }

  return (
    <Box sx={{ display: 'flex', alignItems: 'flex-start', justifyContent: 'space-between' }}>
      <TextField
        variant="filled"
        fullWidth
        error={!!(touched && error)}
        helperText={touched && error}
        label={label}
        name={name}
        value={currentFile?.name ?? ''}
        sx={{ width: '80%' }}
        InputProps={{
          readOnly: true
        }}
        InputLabelProps={{
          shrink: currentFile
        }}
      />
      {
        !readOnly &&
        !file &&
        <Button
          name={name}
          variant="contained"
          component="label"
          sx={{ 'width': '40px', 'minWidth': '40px', 'height': '40px', 'minHeight': '40px', 'marginTop': '7px' }}
          tabindex="-1"
        >
          <Input type='file' onBlur={onBlur} onChange={async (event) => {
            const file = event.target.files[0];
            await handleChange(file);
          }}
          />
          <SvgIcon>
            <ArrowUpOnSquareIcon />
          </SvgIcon>
        </Button>
      }
      {
        file &&
        <Link
          variant="contained"
          href={currentFile && currentFile.base64}
          download={currentFile && currentFile.name}
          sx={{'width': '40px', 'minWidth': '40px', 'height': '40px', 'minHeight': '40px', 'marginTop': '7px', backgroundColor: '#6366F1', display: 'flex', justifyContent: 'center', alignItems: 'center', borderRadius:'12px' }}>
            <SvgIcon sx={{ 'width': '40px', 'minWidth': '40px', 'height': '24px', 'minHeight': '24px', backgroundColor: 'transparent', color: '#ffffff' }}>
            <ArrowDownOnSquareIcon />
          </SvgIcon>
        </Link>
      }
      {
        !readOnly &&
        file &&
        <Button
          name={name}
          variant="contained"
          component="label"
          sx={{ 'width': '40px', 'minWidth': '40px', 'height': '40px', 'minHeight': '40px', 'marginTop': '7px' }}
          tabindex="-1"
          onClick={async () => {
            await handleChange(null);
          }}
        >
          <SvgIcon>
            <TrashIcon />
          </SvgIcon>
        </Button>
      }
    </Box >
  );
}