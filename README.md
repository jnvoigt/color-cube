# Color Characteristics

A Godot-based tool for visualizing the color distribution of images in 3D space. It maps image pixels into predefined or custom color "buckets" within an RGB cube, providing a spatial representation of an image's color characteristics.

## Features

- **3D Color Mapping**: Visualizes colors as spheres within a 3D RGB space.
- **Image Accumulation**: Load any image to see how its pixels are distributed across the available color buckets. Each pixel is assigned to the nearest bucket based on its RGB distance.
- **Average Color Visualization**: Toggle between seeing the bucket's base color and the actual average color of the pixels accumulated within it.
- **Pixel Count Tooltips**: Hover over any bucket to see exactly how many pixels from the image were assigned to it.
- **Custom Buckets**: Add new color buckets on the fly to refine the visualization.
- **Interactive Camera**: Navigate around the 3D space to inspect the color distribution from different angles.

## How It Works

1. **RGB Space**: The project sets up a 3D coordinate system where the X, Y, and Z axes represent Red, Green, and Blue values. The cube is centered at the origin.
2. **Buckets**: Predefined color points (buckets) are placed at their respective RGB coordinates.
3. **Processing**: When an image is loaded, the tool iterates through every pixel and finds the mathematically closest bucket (using squared Euclidean distance in RGB space).
4. **Accumulation**: Each bucket keeps track of the number of pixels assigned to it and calculates their average color.
5. **Visualization**: Buckets are represented by spheres. Their positions in 3D space correspond to their RGB values.

## Getting Started

### Prerequisites

- [Godot Engine 4.x](https://godotengine.org/) (specifically 4.6 or newer, using the .NET/C# version).
- .NET SDK (for C# support).

### Running the Project

1. Clone this repository.
2. Open the project in Godot.
3. Build the C# solution.
4. Run the project (F5).

## Controls

- **Load Image**: Click the "Load Image" button in the menu and select an image file (PNG, JPG, etc.).
- **Rotate Camera**: Hold **Right Mouse Button** and move the mouse.
- **Zoom**: Use the **Mouse Wheel** or the **R** and **F** keys.
- **Toggle Accumulated Color**: Use the switch in the menu to see the average color of pixels in each bucket.
- **Add Color**: Use the color picker and click "Add Color" to create a new bucket at that color's position.
- **Inspect Bucket**: Hover your mouse over a sphere to see the count of pixels assigned to that bucket.

## Technical Details

- **Engine**: Godot 4.6 (Forward Plus)
- **Language**: C# (.NET)
