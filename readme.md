# TextRenderer3D Unity Package

The TextRenderer3D package provides a convenient way to generate 3D text in Unity using a set of customizable models.

## Features

- **Dynamic Text Rendering:** Generate 3D text by writing in the TextRenderer3D component.
- **Customizable:** Adjust properties like spacing, rotation, and letter size to achieve the desired look.
- **Easy Setup:** The package includes a manager class that automatically loads letter models from a specified directory.

## Getting Started

### Add the TextRenderer3D Component:

1. Attach the TextRenderer3D component to a GameObject in your scene.
2. Write the desired text in the text field of the component.

### Optional Manual Manager Setup:

- If you prefer manual setup, press the "Generate" button in the TextRenderer3DManager editor script before using the component.

### Customize Text:

- Adjust properties like spacing, rotation, and letter size in the TextRenderer3D component inspector.

## Usage

The specified 3D text will be generated in the editor using the provided models (the default ones if no additional models are available).

## Folder Structure

- **Custom Editors:** Contains custom editor scripts for the TextRenderer3D and TextRenderer3DManager components.
- **Text Materials:** Stores the materials used for the generated text.
- **Text Models:** Includes a set of pixel art font models for the letters A to Z.

## Setup in Unity Editor

### Add Component:

1. Select a GameObject in your scene.
2. In the Inspector window, click on "Add Component" and search for TextRenderer3D.

### Adjust Properties:

- Customize the properties in the TextRenderer3D component inspector.

### Generate Text:

1. Write the desired text in the text field, and it will dynamically generate.
2. Run the scene to see the generated 3D text.

## Notes

- For automatic manager setup, ensure the TextModels folder is included in your project.

## Contributors

- Paraxodon

## License

This project is licensed under the MIT License.