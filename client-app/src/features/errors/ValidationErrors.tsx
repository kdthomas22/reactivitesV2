import React from "react";
import { Message } from "semantic-ui-react";

interface ValidationProps {
  errors: any;
}

export default function ValidationErrors({ errors }: ValidationProps) {
  return (
    <Message error>
      {errors && (
        <Message.List>
          {errors.map((err: any, i: any) => (
            <Message.Item key={i}>{err}</Message.Item>
          ))}
        </Message.List>
      )}
    </Message>
  );
}
